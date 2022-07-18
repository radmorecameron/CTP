using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeTestingPlatform.DatabaseEntities.Local;

namespace CodeTestingPlatform.Repositories.Interfaces {
    public interface IExceptionRepository {
        Task<List<DatabaseEntities.Local.Exception>> ListAsync(int? LanguageId);
        Task<DatabaseEntities.Local.Exception> FindByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task AddSignatureException(SignatureException signatureExpection);
        Task<List<DatabaseEntities.Local.Exception>> GetSignatureExceptions(int id);
        Task RemoveSignatureExceptions(int id);
        Task AddTestCaseException(TestCaseException testCaseExpection);
        Task<List<DatabaseEntities.Local.Exception>> GetTestCaseException(int id);
        Task RemoveTestCaseException(int id);
    }
}
