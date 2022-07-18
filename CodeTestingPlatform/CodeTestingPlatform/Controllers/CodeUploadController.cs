using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using CodeTestingPlatform.Models.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeTestingPlatform.CompilerClient;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.CompilerClient.Helpers;
using CodeTestingPlatform.Services.Interfaces;
using CodeTestingPlatform.Services;
using CodeTestingPlatform.Models.Validation;

namespace CodeTestingPlatform.Controllers {
    [Authorize(Roles = "ST")]
    [SessionTimeout]
    public class CodeUploadController : Controller {
        private readonly ICurrentSession _cs;
        private readonly CodeUpload _codeUpload;
        private readonly IActivityService _activityService;
        private readonly ITestCaseRepository _testCaseRepository;
        private readonly IResultService _resultService;
        private readonly IMethodSignatureService _signatureService;
        private readonly ICompiler _compiler;
        private readonly ICodeUploadService _codeUploadService;

        public CodeUploadController(ICurrentSession currentSession, ICompiler compiler, CodeUpload codeUpload, IActivityService activityService, ITestCaseRepository testCaseRepository, IMethodSignatureService signatureService, IResultService resultService, ICodeUploadService codeUploadService) {
            _cs = currentSession;
            _codeUpload = codeUpload;
            _activityService = activityService;
            _compiler = compiler;
            _testCaseRepository = testCaseRepository;
            _signatureService = signatureService;
            _resultService = resultService;
            _codeUploadService = codeUploadService;
        }

        [HttpGet]
        public async Task<IActionResult> CodeSubmission(int activityId) {
            var activity = await _activityService.FindByIdAsync(activityId);

            if (activity == null) {
                return NotFound(activityId);
            }

            StudentCodeUpload model = new() { Activity = activity };
            model.SourceFile = new() { ActivityId = activity.ActivityId };

            var codeUpload = await _codeUpload.GetCodeUploadById(_cs.GetStudentId(), activityId);

            if (codeUpload != null) {
                model.FileName = codeUpload.CodeUploadFileName;
                model.FileUploadedDate = (DateTime)codeUpload.UploadDate;
            }

            var invalidTestCases = _activityService.FindInvalidTestCases(activity.MethodSignatures, out _);
            ViewBag.HasOutDatedSignatures = codeUpload?.UploadDate != null ? await _signatureService.UpdatedSignaturesExist((DateTime)codeUpload.UploadDate) : false;

            ViewBag.InvalidTestCases = invalidTestCases;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CodeSubmission(int activityId, string testCaseIdsString) {
            Activity activity = await _activityService.FindByIdAsync(activityId);

            if (activity == null) {
                return NotFound(activityId);
            }

            StudentCodeUpload model = new() { Activity = activity };
            model.SourceFile = new() { ActivityId = activity.ActivityId };

            var tempCodeUpload = await _codeUpload.GetCodeUploadById(_cs.GetStudentId(), activityId);

            if (tempCodeUpload != null) {
                model.FileName = tempCodeUpload.CodeUploadFileName;
                model.FileUploadedDate = (DateTime)tempCodeUpload.UploadDate;
            }

            var invalidTestCases = _activityService.FindInvalidTestCases(activity.MethodSignatures, out _);

            ViewBag.InvalidTestCases = invalidTestCases;

            // Check if no test cases are selected
            if (testCaseIdsString == null) {
                TempData["message"] = "No test cases selected. You must select at least one test case before running tests.";
                return View(model);
            }

            string[] testCaseIds = testCaseIdsString.Split(',');
            List<TestCase> testCases = new();
            foreach (string id in testCaseIds) {
                testCases.Add(await _testCaseRepository.FindByIdAsync(Int32.Parse(id)));
            }

            // Check if student does not have source file in the database
            int studentId = _cs.GetStudentId();
            CodeUpload codeUpload = await _codeUpload.GetCodeUploadById(studentId, activityId);
            if (codeUpload == null) {
                TempData["message"] = "No source file submitted. You must upload a file before running tests.";
                return View(model);
            }

            // Place file in Memory
            using var ms = new MemoryStream();
            InMemoryFile inMemory = new() {
                FileName = "StudentCode.py",
                Content = codeUpload.CodeUploadFile,
            };

            // Submit student code by itself to check syntax errors
            string syntaxCheck = codeUpload.CodeUploadText;
            await _compiler.SubmitCodeAsync(syntaxCheck, 71);
            Submission syntaxCheckSubmission = await _compiler.GetResultsAsync();

            List<Result> results = new();

            // if student's syntax is fine run tests
            if (syntaxCheckSubmission.StandardError == null) {

                // zip student's code
                byte[] zipped = ZipHelper.GetZip(inMemory);
                string additionalFiles = Convert.ToBase64String(zipped);

                // Each test case is added as one submission
                List<Submission> submissions = new();

                // Go through each test case that a student selected
                foreach (TestCase testCase in testCases) {
                    MethodSignature signature = activity.MethodSignatures.SingleOrDefault(x => x.SignatureId == testCase.MethodSignatureId);

                    // Add method signatures to results
                    if (!model.StudentResults.Exists(x => x.MethodSignatureId == signature.SignatureId)) {
                        model.StudentResults.Add(new StudentResult { MethodSignatureId = signature.SignatureId, MethodName = signature.MethodName });
                    }
                    List<SignatureParameter> signatureParameters = signature.SignatureParameters.ToList();
                    List<TestCase> tests = testCases.Where(x => x.MethodSignatureId == testCase.MethodSignatureId).ToList();
                    string filename = inMemory.FileName.Substring(0, inMemory.FileName.IndexOf(".py"));
                    string source = BuilderHelper.BuildSourceFile(filename, signature, signatureParameters, testCase);
                    List<string> values = testCase.Parameters.OrderBy(x => x.SignatureParameter.ParameterPosition).Where(x => x.SignatureParameter.DataType.DataType1 != "list").ToList().ConvertAll(x => x.Value);
                    string inputValues = string.Join("\n", values);
                    submissions.Add(SubmissionHelper.GetSubmission(source, 71, additionalFiles, inputValues, testCase.ExpectedValue));
                    results.Add(new Result {
                        TestCaseId = testCase.TestCaseId,
                        TestCase = testCase,
                        //ActivityId = activity.ActivityId,
                        CodeUploadId = codeUpload.CodeUploadId,
                        CodeUpload = codeUpload
                    });
                }

                // submit all submissions as a batch and get the response for all submissions
                await _compiler.SubmitCodeBatchAsync(submissions);
                List<Submission> compiled = await _compiler.GetResultsBatchAsync();

                // check the results
                int index = 0;
                foreach (Submission sub in compiled) {
                    results.ElementAt(index).PassFail = ResultHelper.PassOrFailResult(sub);
                    results.ElementAt(index).ErrorMessage = sub.StandardError;
                    results.ElementAt(index).ActualValue = sub.StandardOutput?.Trim();
                    index++;
                }
                model.Results = results;
                ViewBag.Result = results.Count;
            } else {
                results.Add(
                    new Result {
                        ErrorMessage = syntaxCheckSubmission.StandardError,
                        PassFail = false,
                    });
                model.Results = results;
                ViewBag.Result = results.Count;
            }

            // Increment passed or failed test cases for each method
            foreach (var studentResult in model.StudentResults) {
                List<Result> methodResults = results.Where(x => x.TestCase.MethodSignatureId == studentResult.MethodSignatureId).ToList();
                foreach (var result in methodResults) {
                    if (result.PassFail.Value) {
                        studentResult.Passed += 1;
                    } else {
                        studentResult.Failed += 1;
                    }
                }
            }

            ViewBag.HasOutDatedSignatures = await _signatureService.UpdatedSignaturesExist((DateTime)codeUpload.UploadDate);

            // Test Case Results Data Persistence
            await _resultService.CreateAsync(codeUpload.CodeUploadId, results.ToArray());

            return View(model);
        }

        public IActionResult SourceFile() {
            return View("CodeSubmission", new StudentCodeUpload());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SourceFile(SourceFile sourceFile) {
            var activity = await _activityService.FindByIdAsync(sourceFile.ActivityId);

            if (activity == null) {
                return NotFound(sourceFile.ActivityId);
            }

            if (ModelState.IsValid) {
                // Only one file per activity per student can be submitted. Override file if a file exists.
                int studentId = _cs.GetStudentId();
                CodeUpload codeUpload = await _codeUpload.GetCodeUploadById(studentId, sourceFile.ActivityId);

                if (codeUpload == null || (codeUpload.CodeUploadFile == null && codeUpload.CodeUploadText == null)) {
                    // add a code 
                    await _codeUpload.AddSourceFileAsync(sourceFile, studentId);
                } else {
                    // update code
                    await _codeUpload.UpdateSourceFileAsync(sourceFile, studentId, codeUpload.CodeUploadId);
                }
            }

            return RedirectToAction("CodeSubmission", new { activity.ActivityId });
        }

        public IActionResult SourceCode() {
            return View("CodeSubmission", new StudentCodeUpload());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SourceCode(SourceCode sourceCode) {
            if (ModelState.IsValid) {
                return View("CodeSubmission", new StudentCodeUpload());
            }

            return View("CodeSubmission", new StudentCodeUpload());
        }
    }
}
