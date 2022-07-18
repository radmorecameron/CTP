using CodeTestingPlatform.DatabaseEntities.Local;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeTestingPlatform.Services.Interfaces;
using CodeTestingPlatform.Models.Validation.LanguageValidators;
using CodeTestingPlatform.Models.Validation;
using System.Linq;
using CodeTestingPlatform.Models;
using System;
using CodeTestingPlatform.Models.Enums;
using System.Text.Json;

namespace CodeTestingPlatform.Controllers {
    [Authorize(Roles = "TE")]
    [SessionTimeout]
    public class MethodSignatureController : Controller {
        private readonly IMethodSignatureService _signatureService;
        private readonly IActivityService _activityService;
        private readonly IDataTypeService _dataTypeService;
        private readonly IParameterService _parameterService;
        private readonly ITestCaseService _testCaseService;
        private readonly IExceptionService _exceptionService;
        private readonly IUserDefinedExceptionService _userDefinedExceptionService;

        public MethodSignatureController(IMethodSignatureService signatureService, IActivityService activityService, IDataTypeService dataTypeService, IParameterService parameterService, ITestCaseService testCaseService, IExceptionService exceptionService, IUserDefinedExceptionService userDefinedExceptionService) {
            _signatureService = signatureService;
            _activityService = activityService;
            _dataTypeService = dataTypeService;
            _parameterService = parameterService;
            _testCaseService = testCaseService;
            _exceptionService = exceptionService;
            _userDefinedExceptionService = userDefinedExceptionService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id, string from = "Activity") {
            if (id == null) {
                return NotFound();
            }

            Activity activity = await _activityService.FindByIdAsync(id.Value);

            if (activity == null) {
                return NotFound(id);
            }

            ViewBag.Exceptions = await _exceptionService.ListAsync(activity.LanguageId);
            ViewBag.UserDefinedExceptions = await _userDefinedExceptionService.ListAsync(activity.LanguageId);

            IEnumerable<DataType> dataTypes = await _dataTypeService.ListAsync(activity.LanguageId);
            ViewData["DataTypes"] = new SelectList(dataTypes, "DataTypeId", "DataType1");

            // Default parameter
            var parameters = new SignatureParameter[1] {
                new SignatureParameter {
                    //ParameterName = "",
                    DataTypeId = -1,
                    //RequiredParameter = true,
                    InputParameter = true
                }
            };

            ViewData["from"] = from;
            MethodSignature model = new() { ActivityId = id.Value, Activity = activity, SignatureParameters = parameters };
            return View("Manage", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MethodSignature model, SignatureParameter[] parameters, string from, string exceptionIdsString, string userDefinedExceptionIdsString) {
            from = from == "" ? "Activity" : from;
            Activity activity = await _activityService.FindByIdAsync(model.ActivityId);

            if (model == null) {
                ModelState.AddModelError("error", "Input Model is null");
                return BadRequest(ModelState);
            }

            ViewBag.Exceptions = await _exceptionService.ListAsync(activity.LanguageId);
            ViewBag.UserDefinedExceptions = await _userDefinedExceptionService.ListAsync(activity.LanguageId);

            if (activity.Language.LanguageName == "Python") {
                foreach (var param in parameters) {
                    //Add capital letter to bool
                    if (param.DefaultValue == null) {
                        param.RequiredParameter = true;
                    }
                    if (param.DefaultValue == "true") {
                        param.DefaultValue = "True";
                    }
                    if (param.DefaultValue == "false") {
                        param.DefaultValue = "False";
                    }
                }
            }

            //Check for empty param name
            foreach (var param in parameters) {
                if (param.ParameterName == null) {
                    ModelState.AddModelError("Parameter", $"Parameter names missing");
                    break;
                }
            }

            List<DataType> dataTypes = (await _dataTypeService.ListAsync(activity.LanguageId)).ToList();

            List<string> parameterErrors = new();
            ILanguageValidator validator = LanguageValidator.GetLanguageValidator(activity.LanguageId);
            if (validator != null) {
                ValidatorResult validName = validator.IsValidMethodName(model.MethodName);
                if (validName.Result == ValidatorResult.ResultEnum.Error) {
                    ModelState.AddModelError("Method Name", validName.Message);
                }
                if (parameters.Length < 1) {
                    ModelState.AddModelError("Parameter", "The method needs at least one parameter.");
                } else {
                    parameterErrors = GetParamterErrors(parameters, dataTypes, validator);
                }
            }

            if (parameterErrors.Count != 0) {
                foreach(var error in parameterErrors) {
                    ModelState.AddModelError("Parameter", error);
                }
            }
            ViewBag.ParameterErrors = parameterErrors;
            model.FilterReturnObjType();
            model.SignatureParameters = MethodSignature.FilterObjType(parameters);

            if (!ModelState.IsValid) {
                TempData["errorMessage"] = string.Join("<br>&nbsp;&nbsp;&nbsp;&nbsp; -", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));


                ViewData["DataTypes"] = new SelectList(dataTypes, "DataTypeId", "DataType1", model.ReturnTypeId);
                ViewData["from"] = from;

                // Default parameter
                //model.SignatureParameters = parameters;
                model.Activity = activity;
                return View("Manage", model);
            }

            
            await _signatureService.CreateAsync(model);

            TempData["message"] = $"Successfully created a signature {model.MethodName} for the activity {activity.Title}";

            if (exceptionIdsString != null) {
                string[] exceptionIds = exceptionIdsString.Split(',');
                foreach (string id in exceptionIds) {
                    await _exceptionService.AddSignatureException(new SignatureException(model.SignatureId, Int32.Parse(id)));
                }
            }

            if (userDefinedExceptionIdsString != null) {
                string[] userDefinedExceptionIds = userDefinedExceptionIdsString.Split(',');
                foreach (string id in userDefinedExceptionIds) {
                    await _userDefinedExceptionService.AddSignatureUserDefinedException(new SignatureUserDefinedException(model.SignatureId, Int32.Parse(id)));
                }
            }

            if (from == "Course") {
                return RedirectToAction("Details", "Course", new { id = activity.CourseId });
            }
            return RedirectToAction("Details", "MethodSignature", new { id = model.SignatureId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) {
            MethodSignature signature = await _signatureService.FindByIdAsync(id);
            if (signature == null) {
                return NotFound(id);
            }

            Activity activity = await _activityService.FindByIdAsync(signature.ActivityId);
            IEnumerable<DataType> dataTypes = await _dataTypeService.ListAsync(activity.LanguageId);
            ViewData["DataTypes"] = new SelectList(dataTypes, "DataTypeId", "DataType1", signature.ReturnTypeId);
            ViewBag.Exceptions = await _exceptionService.ListAsync(activity.LanguageId);
            ViewBag.SignatureExceptions = (await _exceptionService.GetSignatureExceptions(id)).Select(e => e.ExceptionId).ToList();

            ViewBag.UserDefinedExceptions = await _userDefinedExceptionService.ListAsync(activity.LanguageId);
            ViewBag.SignatureUserDefinedExceptions = (await _userDefinedExceptionService.GetSignatureUserDefinedExceptions(id)).Select(e => e.UserDefinedExceptionId).ToList();

            return View("Manage", signature);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MethodSignature model, SignatureParameter[] parameters, string exceptionIdsString, string userDefinedExceptionIdsString) {
            if (model == null) {
                ModelState.AddModelError("error", "Input Model is null");
                return BadRequest(ModelState);
            }
            model.TestCases = await _testCaseService.ListAsync(model.SignatureId);
            model.FilterReturnObjType();
            Activity activity = await _activityService.FindByIdAsync(model.ActivityId);
            ViewBag.Exceptions = await _exceptionService.ListAsync(activity.LanguageId);
            ViewBag.SignatureExceptions = (await _exceptionService.GetSignatureExceptions(id)).Select(e => e.ExceptionId).ToList();
            ViewBag.UserDefinedExceptions = await _userDefinedExceptionService.ListAsync(activity.LanguageId);
            ViewBag.SignatureUserDefinedExceptions = (await _userDefinedExceptionService.GetSignatureUserDefinedExceptions(id)).Select(e => e.UserDefinedExceptionId).ToList();
            IList<DataType> dataTypes = await _dataTypeService.ListAsync(activity.LanguageId);
            ViewData["DataTypes"] = new SelectList(dataTypes, "DataTypeId", "DataType1", model.ReturnTypeId);
            MethodSignature signature = await _signatureService.FindByIdAsync(id);

            if (signature.Activity.Language.LanguageName == "Python") {
                foreach (var param in parameters) {
                    if (param.DefaultValue == null) {
                        param.RequiredParameter = true;
                    }
                    if (param.DefaultValue == "true") {
                        param.DefaultValue = "True";
                    }
                    if (param.DefaultValue == "false") {
                        param.DefaultValue = "False";
                    }
                }
            }

            List<string> parameterErrors = new();
            ILanguageValidator validator = LanguageValidator.GetLanguageValidator(activity.LanguageId);
            if (validator != null) {
                ValidatorResult validName = validator.IsValidMethodName(model.MethodName);
                if (validName.Result == ValidatorResult.ResultEnum.Error) {
                    ModelState.AddModelError("Method Name", validName.Message);
                }
                if (parameters.Length < 1) {
                    ModelState.AddModelError("Parameter", "The method needs at least one parameter.");
                } else {
                    parameterErrors = GetParamterErrors(parameters, dataTypes, validator);
                }
            }
            if (parameterErrors.Count != 0) {
                foreach (var error in parameterErrors) {
                    ModelState.AddModelError("Parameter", error);
                }
            }
            ViewBag.ParameterErrors = parameterErrors;
            signature = await _signatureService.FindByIdAsync(id);
            if (!ModelState.IsValid) {
                TempData["errorMessage"] = string.Join("<br>&nbsp;&nbsp;&nbsp;&nbsp; -", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));


                model.Activity = activity;
                model.SignatureParameters = parameters;
                return View("Manage", model);
            }

            if (signature == null) {
                return NotFound(model.SignatureId);
            }

            model.SignatureParameters = MethodSignature.FilterObjType(parameters);

            List<string> parameterNames = model.SignatureParameters.Select(sp => sp.ParameterName).ToList();
            List<UpdateParameter> parametersToUpdate = new();
            List<UpdateParameter> parametersToCreate = new();
            foreach (TestCase testCase in model.TestCases) {

                foreach (SignatureParameter sigParameter in model.SignatureParameters) {
                    Parameter parameter = testCase.Parameters.Where(p => p.SignatureParameter.ParameterName == sigParameter.ParameterName).FirstOrDefault();
                    if (parameter != null) {
                        UpdateParameter updateParam = new() { Parameter = parameter, PreviousParameterName = sigParameter.ParameterName };
                        parametersToUpdate.Add(updateParam);
                    } else {
                        UpdateParameter createParam = new() { Parameter = new Parameter() { TestCaseId = testCase.TestCaseId }, PreviousParameterName = sigParameter.ParameterName };
                        parametersToCreate.Add(createParam);
                    }
                }
                var paramsToRemove = testCase.Parameters.Where(p => parameterNames.Contains(p.SignatureParameter.ParameterName) == false).ToList();
                foreach (Parameter param in paramsToRemove) {
                    await _parameterService.DeleteAsync(param);
                }
            }

            await _signatureService.UpdateAsync(model);

            foreach (var param in parametersToUpdate) {
                param.Parameter.SignatureParameterId = model.SignatureParameters.Where(sp => sp.ParameterName == param.PreviousParameterName).FirstOrDefault().SignatureParameterId;
                await _parameterService.UpdateAsync(param.Parameter);
            }

            foreach (var param in parametersToCreate) {
                param.Parameter.SignatureParameterId = model.SignatureParameters.Where(sp => sp.ParameterName == param.PreviousParameterName).FirstOrDefault().SignatureParameterId;
                await _parameterService.CreateAsync(param.Parameter);
            }

            model.Activity = activity;

            await _exceptionService.RemoveSignatureExceptions(model.SignatureId);
            await _userDefinedExceptionService.RemoveSignatureUserDefinedExceptions(model.SignatureId);

            if (exceptionIdsString != null) {
                string[] exceptionIds = exceptionIdsString.Split(',');
                foreach (string exceptionId in exceptionIds) {
                    await _exceptionService.AddSignatureException(new SignatureException(model.SignatureId, Int32.Parse(exceptionId)));
                }
            }

            if (userDefinedExceptionIdsString != null) {
                string[] userDefinedExceptionIds = userDefinedExceptionIdsString.Split(',');
                foreach (string userDefinedExceptionId in userDefinedExceptionIds) {
                    await _userDefinedExceptionService.AddSignatureUserDefinedException(new SignatureUserDefinedException(model.SignatureId, Int32.Parse(userDefinedExceptionId)));
                }
            }

            TempData["message"] = "Successfully modified the signature " + model.MethodName;
            return RedirectToAction("Details", new { id = model.SignatureId });
        }

        public async Task<IActionResult> Details(int id) {
            MethodSignature signature = await _signatureService.FindByIdAsync(id);
            if (signature == null) {
                return NotFound();
            }
            InvalidTestCases(signature);
            ViewBag.TestCases = await _testCaseService.FindByIdAsync(id);
            var signatureExceptions = signature.SignatureExceptions.Select(e => e.Exception.ExceptionName).ToList();
            signature.SignatureUserDefinedExceptions.ToList().ForEach(s => signatureExceptions.Add(s.UserDefinedException.UserDefinedExceptionName));
            signatureExceptions.OrderBy(s => s);
            ViewBag.Exceptions = signatureExceptions;
            return View(signature);
        }

        // GET: Signature/Delete/
        public async Task<IActionResult> Delete(int id) {
            MethodSignature signature = await _signatureService.FindByIdAsync(id);
            if (signature == null) {
                return NotFound();
            }
            return View(signature);
        }

        //POST: Signature/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            MethodSignature signature = await _signatureService.FindByIdAsync(id);
            if (signature == null) {
                return NotFound();
            }
            await _signatureService.DeleteAsync(signature);
            return RedirectToAction("Details", "Activity", new { id = signature.ActivityId });
        }

        private void InvalidTestCases(MethodSignature signature) {
            Dictionary<int, List<string>> invalidTestCases = _signatureService.FindInvalidTestCases(signature.TestCases);
            ViewBag.InvalidTestCases = invalidTestCases;
        }

        private List<string> GetParamterErrors(SignatureParameter[] parameters, IList<DataType> dataTypes, ILanguageValidator validator) {
            bool CanBeRequired = parameters[0].RequiredParameter;
            List<string> paramNames = new();
            List<string> parameterErrors = new();
            foreach (SignatureParameter param in parameters) {
                if (param.ParameterName != null) {
                    ValidatorResult validParam = validator.IsValidParameter(param);
                    if (validParam.Result == ValidatorResult.ResultEnum.Error) {
                        parameterErrors.Add(validParam.Message);
                    }
                    if (param.DefaultValue != null) {
                        string dataType = dataTypes.FirstOrDefault(d => d.DataTypeId == param.DataTypeId)?.DataType1;
                        if (dataType != null) {
                            if (!ValueDataTypeValidator.CheckParamDataType(param.DefaultValue, dataType)) {
                                parameterErrors.Add($"The default value of \"{param.DefaultValue}\" is not of type {dataType}");
                            }
                        }
                    }
                    if (!param.RequiredParameter) {
                        CanBeRequired = false;
                    }
                    if (param.RequiredParameter && !CanBeRequired) {
                        parameterErrors.Add($"{param.ParameterName} should be before the non-required parameters");
                    }
                    if (paramNames.FirstOrDefault(p => p == param.ParameterName) != null) {

                        parameterErrors.Add($"Multiple parameters with {param.ParameterName} as name");
                    }
                    paramNames.Add(param.ParameterName);
                }
            }
            return parameterErrors;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> AddUserDefinedException(string userDefinedExceptionName, int languageId, int signatureId) {
            bool hasLang = languageId! > 0;
            bool hasSign = signatureId !> 0;
            bool noErrors = !string.IsNullOrWhiteSpace(userDefinedExceptionName) && languageId !> 0 && signatureId !> 0;
            string errorMessage = string.Empty;
            UserDefinedException userDefinedException = new();
            if (noErrors) {
                var exceptions = await _userDefinedExceptionService.ListAsync(languageId);
                var hasException = exceptions.Where(x => x.UserDefinedExceptionName == userDefinedExceptionName).SingleOrDefault();
                if (hasException == null) {
                    await _userDefinedExceptionService.AddUserDefinedException(new UserDefinedException { UserDefinedExceptionName = userDefinedExceptionName, LanguageId = languageId});
                    exceptions = await _userDefinedExceptionService.ListAsync(languageId);
                    userDefinedException = exceptions.Where(x => x.UserDefinedExceptionName == userDefinedExceptionName).SingleOrDefault();
                } else {
                    errorMessage = $"There is already an exception with the name {userDefinedExceptionName}.";
                }

                
            } else {
                if (string.IsNullOrEmpty(userDefinedExceptionName))
                    errorMessage = $"Exception name is required.";
                else if (string.IsNullOrWhiteSpace(userDefinedExceptionName))
                    errorMessage = "Exception name should not contain spaces.";
                else
                    errorMessage = "Adding exception was not successful.";
            }

            return Json(new { data = userDefinedException, error = errorMessage});
        }
    }
}
