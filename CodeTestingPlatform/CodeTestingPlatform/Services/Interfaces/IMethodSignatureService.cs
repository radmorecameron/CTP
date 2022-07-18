using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services.Interfaces {
    public interface IMethodSignatureService {
        Task<MethodSignature> FindByIdAsync(int id);
        Task<SignatureParameter> FindSignatureParameterByIdAsync(int id);
        Task<List<MethodSignature>> ListAsync(int? activityId);
        Task CreateAsync(MethodSignature signature);
        Task UpdateAsync(MethodSignature signature);
        Task DeleteAsync(MethodSignature signature);
        Task<bool> ExistsAsync(int id);
        Dictionary<int, List<string>> FindInvalidTestCases(IEnumerable<TestCase> testCases);
        Task<bool> UpdatedSignaturesExist(DateTime codeUploadDate);
    }
}
