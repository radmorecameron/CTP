using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models.Validation;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public class MethodSignatureService : IMethodSignatureService {
        private readonly IMethodSignatureRepository _signatureRepository;
        public MethodSignatureService(IMethodSignatureRepository signatureRepository) {
            _signatureRepository = signatureRepository;
        }

        public async Task CreateAsync(MethodSignature signature) {
            await _signatureRepository.CreateAsync(signature);
        }

        public async Task DeleteAsync(MethodSignature signature) {
            await _signatureRepository.DeleteAsync(signature);
        }

        public async Task<MethodSignature> FindByIdAsync(int id) {
            return await _signatureRepository.FindByIdAsync(id);
        }

        public async Task<SignatureParameter> FindSignatureParameterByIdAsync(int id) {
            return await _signatureRepository.FindSignatureParameterByIdAsync(id);
        }

        public async Task<List<MethodSignature>> ListAsync(int? activityId) {
            return await _signatureRepository.ListAsync(activityId);
        }

        public async Task UpdateAsync(MethodSignature signature) {
            await _signatureRepository.UpdateAsync(signature);
        }

        public async Task<bool> ExistsAsync(int id) {
            return await _signatureRepository.ExistsAsync(id);
        }

        public Dictionary<int, List<string>> FindInvalidTestCases(IEnumerable<TestCase> testCases) {
            Dictionary<int, List<string>> invalidTestCases = new();
            foreach (TestCase tc in testCases) {
                if (tc.ValidateTestCase) {
                    bool isValid = true;
                    List<string> errorMessages = new();
                    foreach (Parameter param in tc.Parameters) {
                        if (param.Value == null && param.SignatureParameter.RequiredParameter) { // If param is missing and signature parameter is required
                            isValid = false;
                            errorMessages.Add($"{param.SignatureParameter.ParameterName}: Missing value for required field. <br> ");
                        } else if (!ValueDataTypeValidator.CheckParamDataType(param.Value, param.SignatureParameter.DataType.DataType1) && param.SignatureParameter.RequiredParameter) { // If Param does not follow correct DataType
                            isValid = false;
                            errorMessages.Add($"{param.SignatureParameter.ParameterName}: Doesn't match Data Type ({param.SignatureParameter.DataType.DataType1}). <br> ");
                        }
                    }
                    if (!ValueDataTypeValidator.CheckParamDataType(tc.ExpectedValue, tc.MethodSignature.ReturnType.DataType1)) {
                        isValid = false;
                        errorMessages.Add($"Expected Result: Doesn't match Data Type ({tc.MethodSignature.ReturnType.DataType1}). <br> ");
                    }
                    if (!isValid)
                        invalidTestCases.Add(tc.TestCaseId, errorMessages);
                }
            }
            return invalidTestCases;
        }

        public async Task<bool> UpdatedSignaturesExist(DateTime codeUploadDate) {
            return (await _signatureRepository.FindUpdatedSignatures(codeUploadDate)).Count() > 0;
        }
    }
}
