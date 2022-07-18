using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services.Interfaces {
    public interface IUserDefinedExceptionService {
        Task<UserDefinedException> FindByIdAsync(int id);
        Task<List<UserDefinedException>> ListAsync(int languageId);
        Task<bool> ExistsAsync(int id);
        Task AddSignatureUserDefinedException(SignatureUserDefinedException signatureUserDefinedException);
        Task<List<UserDefinedException>> GetSignatureUserDefinedExceptions(int id);
        Task RemoveSignatureUserDefinedExceptions(int id);
        Task<int> AddUserDefinedExceptionByName(string name, int languageId);
        Task AddUserDefinedException(UserDefinedException userDefinedException);
    }
}
