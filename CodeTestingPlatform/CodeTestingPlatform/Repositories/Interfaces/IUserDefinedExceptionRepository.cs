using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories.Interfaces {
    public interface IUserDefinedExceptionRepository {
        Task<List<UserDefinedException>> ListAsync(int? LanguageId);
        Task<UserDefinedException> FindByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task AddSignatureUserDefinedException(SignatureUserDefinedException signatureExpection);
        Task<List<UserDefinedException>> GetSignatureUserDefinedExceptions(int id);
        Task RemoveSignatureUserDefinedExceptions(int id);
        Task<int> AddUserDefinedExceptionByName(string name, int languageId);
        Task AddUserDefinedException(UserDefinedException userDefinedException);
    }
}
