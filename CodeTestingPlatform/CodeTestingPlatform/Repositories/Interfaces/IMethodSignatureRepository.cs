using CodeTestingPlatform.DatabaseEntities.Local;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories.Interfaces {
    public interface IMethodSignatureRepository {
        Task<MethodSignature> FindByIdAsync(int id);
        Task<SignatureParameter> FindSignatureParameterByIdAsync(int id);
        Task<List<MethodSignature>> ListAsync(int? activityId);
        Task CreateAsync(MethodSignature signature);
        Task UpdateAsync(MethodSignature signature);
        Task DeleteAsync(MethodSignature signature);
        Task<bool> ExistsAsync(int id);
        Task UpdateDate(int id);
        Task<List<MethodSignature>> FindUpdatedSignatures(DateTime codeUploadDate);
    }
}
