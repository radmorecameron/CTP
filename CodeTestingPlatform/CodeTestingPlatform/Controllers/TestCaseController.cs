using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Models.Validation;
using CodeTestingPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Controllers {
    [Authorize(Roles = "TE")]
    [SessionTimeout]
    public class TestCaseController : Controller {
        private readonly ITestCaseService _testCaseService;
        private readonly IMethodSignatureService _signatureService;
        private readonly IDataTypeService _dataTypeService;
        private readonly IParameterService _parameterService;
        private readonly IExceptionService _exceptionService;

        public TestCaseController(ITestCaseService testCaseService, IMethodSignatureService signatureService, IDataTypeService dataTypeService, IParameterService parameterService, IExceptionService exceptionService) {
            _testCaseService = testCaseService;
            _signatureService = signatureService;
            _dataTypeService = dataTypeService;
            _parameterService = parameterService;
            _exceptionService = exceptionService;
        }

        public async Task<IActionResult> Index() {
            return View(await _testCaseService.ListAsync());
        }

        // GET : TestCase/Create/5
        [HttpGet]
        public async Task<IActionResult> Create(int? id, string from) {
            ViewData["from"] = from;
            ViewData["courseId"] = TempData["courseId"];
            ViewData["activityId"] = TempData["activityId"];
            if (id == null) {
                return NotFound();
            }
            ViewBag.MethodSignatureId = id;

            var signature = await _signatureService.FindByIdAsync(id.Value);

            ViewBag.MethodSignature = signature;
            ViewBag.TestCaseExceptions = signature.SignatureExceptions;
            ViewBag.SelectedException = -1;
            return View(new TestCase());
        }

        // POST : TestCase/Create/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestCase testCase, Parameter[] parameters, int testCaseException) {
            ViewData["from"] = TempData["from"];
            ViewData["activityId"] = TempData["activityId"];
            ViewData["courseId"] = TempData["courseId"];
            ViewBag.MethodSignatureId = TempData["methodId"];
            var signature = await _signatureService.FindByIdAsync(testCase.MethodSignatureId);
            testCase.Parameters = parameters;
            testCase.MethodSignature = signature;

            try {
                if (testCaseException == -1 && testCase.ExpectedValue == null) {
                    ModelState.AddModelError("ExpectedValue", "The Expected value field is required.");
                }
                if (testCase.TestCaseName != null) {
                    var notUniqueTestCase = await _testCaseService.FindBySigIdAndNameAsync(testCase.MethodSignatureId, testCase.TestCaseName);
                    if (notUniqueTestCase != null) {
                        ModelState.AddModelError("TestCaseName", "Test Case name already used.");
                    }
                }
                List<string> parameterErrors = new();
                bool returnTypePass = true;
                bool paramPass = true;
                bool exceptionReturned = false;

                testCase.Parameters = parameters;
                DataType dt = new();

                if (testCase.ValidateTestCase) {
                    if (!ValueDataTypeValidator.CheckParamDataType(testCase.ExpectedValue, testCase.MethodSignature.ReturnType.DataType1)) {
                        returnTypePass = false;
                        parameterErrors.Add($"Incorrect data type for the expected value.");
                    }

                    foreach (var param in parameters) {
                        SignatureParameter sigParam = await _signatureService.FindSignatureParameterByIdAsync((int)param.SignatureParameterId);
                        if (!ValueDataTypeValidator.CheckParamDataType(param.Value, sigParam.DataType.DataType1)) {
                            paramPass = false;
                            parameterErrors.Add($"Incorrect data type for {sigParam.ParameterName}.");
                        }
                        if (param.Value == null && sigParam.DefaultValue == null) {
                            parameterErrors.Add($"The field {sigParam.ParameterName} is required.");
                        }

                        param.SignatureParameter = !paramPass ? sigParam : null;
                    }

                    if (parameterErrors.Count != 0) {
                        foreach (var error in parameterErrors) {
                            ModelState.AddModelError("Parameter", error);
                        }
                    }

                    ViewBag.ParameterErrors = parameterErrors;
                } 
                if (testCaseException != -1) {
                    exceptionReturned = true;
                    testCase.ExpectedValue = null;
                }

                if (ModelState.IsValid) {
                    if (paramPass && returnTypePass) {
                        await _testCaseService.CreateAsync(testCase);
                        if (exceptionReturned) {
                            await _exceptionService.AddTestCaseException(new TestCaseException(testCase.TestCaseId, testCaseException));
                        }
                        TempData["message"] = $"Successfully created the test case {testCase.TestCaseName}";
                        return RedirectToAction("Details", "MethodSignature", new { id = testCase.MethodSignatureId });
                    }
                } else {
                    TempData["errorMessage"] = string.Join("<br>&nbsp;&nbsp;&nbsp;&nbsp; -", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                }
            } catch (DbUpdateConcurrencyException exc) {
                ExceptionLogger.LogException(exc, "TestCaseController");
                throw;
            }

            ViewBag.MethodSignature = signature;
            ViewBag.TestCaseExceptions = signature.SignatureExceptions;
            ViewBag.SelectedException = testCaseException;
            return View(testCase);
        }

        // GET : TestCase/5
        [HttpGet]
        public async Task<IActionResult> Details(int id) {
            TestCase testCaseObj = await _testCaseService.FindByIdAsync(id);

            if (testCaseObj == null)
                return NotFound();

            return View(testCaseObj);
        }

        // GET : TestCase/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id) {
            TestCase testCaseObj = await _testCaseService.FindByIdAsync(id);
            if (testCaseObj == null) {
                return NotFound();
            }

            var signature = await _signatureService.FindByIdAsync(testCaseObj.MethodSignatureId);
            ViewBag.MethodSignature = signature;
            ViewBag.SignatureParameters = signature.SignatureParameters.ToList();
            ViewBag.TestCaseExceptions = signature.SignatureExceptions;
            ViewBag.SelectedException = testCaseObj.TestCaseException?.ExceptionId ?? -1;

            ViewData["OriginalTestCaseName"] = testCaseObj.TestCaseName;
            return View(testCaseObj);
        }

        // POST : TestCase/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TestCase testCase, Parameter[] parameters, int testCaseException) {
            var signature = await _signatureService.FindByIdAsync(testCase.MethodSignatureId);
            ViewBag.MethodSignature = signature;
            ViewBag.SignatureParameters = signature.SignatureParameters.ToList();

            ViewData["OriginalTestCaseName"] = TempData["OriginalTestCaseName"];

            if (id != testCase.TestCaseId) {
                return NotFound();
            }

            try {
                if (testCaseException == -1 && testCase.ExpectedValue == null) {
                    ModelState.AddModelError("ExpectedValue", "The Expected value field is required.");
                }
                if (testCase.TestCaseName != null) {
                    var notUniqueTestCase = await _testCaseService.FindBySigIdAndNameAsync(testCase.MethodSignatureId, testCase.TestCaseName);
                    if (notUniqueTestCase != null && testCase.TestCaseName != TempData["OriginalTestCaseName"].ToString()) {
                        ModelState.AddModelError("TestCaseName", "Test Case name already used.");
                    }
                }

                List<string> parameterErrors = new();
                bool paramPass = true;
                bool returnTypePass = true;
                bool exceptionReturned = false;

                DataType dt = new();

                if (testCase.ValidateTestCase) {
                    if (!ValueDataTypeValidator.CheckParamDataType(testCase.ExpectedValue, signature.ReturnType.DataType1)) {
                        returnTypePass = false;
                        parameterErrors.Add($"Incorrect data type for the expected value.");
                    }

                    foreach (var param in parameters) {
                        var sigParameter = await _signatureService.FindSignatureParameterByIdAsync((int)param.SignatureParameterId);
                        param.SignatureParameter = sigParameter;
                        if (!ValueDataTypeValidator.CheckParamDataType(param.Value, sigParameter.DataType.DataType1)) {
                            paramPass = false;
                            parameterErrors.Add($"Incorrect data type for " + sigParameter.ParameterName);
                        }
                        if (param.Value == null && sigParameter.DefaultValue == null) {
                            paramPass = false;
                            parameterErrors.Add($"The field {sigParameter.ParameterName} is required.");
                        }
                    }

                    if (parameterErrors.Count != 0) {
                        foreach (var error in parameterErrors) {
                            ModelState.AddModelError("Parameter", error);
                        }
                    }
                    ViewBag.ParameterErrors = parameterErrors;
                } 
                if (testCaseException != -1) {
                    exceptionReturned = true;
                    testCase.ExpectedValue = null;
                }

                if (ModelState.IsValid) {
                    if (returnTypePass && paramPass) {
                        _parameterService.DetachEntities();
                        for (var i = 0; i < parameters.Count(); i++) {
                            parameters[i].SignatureParameter = null;
                            await _parameterService.UpdateAsync(parameters[i]);
                        }
                        _parameterService.DetachEntities();
                        await _exceptionService.RemoveTestCaseException(testCase.TestCaseId);
                        if (exceptionReturned)
                            await _exceptionService.AddTestCaseException(new TestCaseException(testCase.TestCaseId, testCaseException));
                        await _testCaseService.UpdateAsync(testCase);
                        TempData["message"] = "Successfully modified the test case " + testCase.TestCaseName;
                        return RedirectToAction("Details", new { id = testCase.TestCaseId });
                    }
                } else {
                    TempData["errorMessage"] = string.Join("<br>&nbsp;&nbsp;&nbsp;&nbsp; -", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                }
            } catch (DbUpdateConcurrencyException exc) {
                ExceptionLogger.LogException(exc, "TestCaseController");
                throw;
            }

            foreach (var param in parameters) {
                param.SignatureParameter = await _signatureService.FindSignatureParameterByIdAsync((int)param.SignatureParameterId);
            }

            testCase.Parameters = parameters;
            ViewBag.TestCaseExceptions = signature.SignatureExceptions;
            ViewBag.SelectedException = testCaseException;

            return View(testCase);
        }

        // GET: TestCase/Delete/
        public async Task<IActionResult> Delete(int id) {
            TestCase testCaseObj = await _testCaseService.FindByIdAsync(id);
            if (testCaseObj == null) {
                return NotFound();
            }
            return View(testCaseObj);
        }

        //POST: TestCase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            TestCase testCaseObj = await _testCaseService.FindByIdAsync(id);
            await _testCaseService.DeleteAsync(testCaseObj);
            return RedirectToAction("Details", "MethodSignature", new { id = testCaseObj.MethodSignatureId });
        }

        private ICollection<Parameter> GetParameterDetails(ICollection<Parameter> parameters, ICollection<SignatureParameter> signatureParameters) {
            foreach (SignatureParameter sp in signatureParameters) {
                parameters.ElementAt((int)sp.ParameterPosition).SignatureParameter = sp;
            }
            return parameters;
        }
    }
}
