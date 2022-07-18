using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services.Interfaces {
    public interface IExceptionService {
        Task<DatabaseEntities.Local.Exception> FindByIdAsync(int id);
        Task<IList<DatabaseEntities.Local.Exception>> ListAsync(int languageId);
        Task<bool> ExistsAsync(int id);
        Task AddSignatureException(SignatureException signatureExpection);
        Task<List<DatabaseEntities.Local.Exception>> GetSignatureExceptions(int id);
        Task RemoveSignatureExceptions(int id);
        Task AddTestCaseException(TestCaseException testCaseExpection);
        Task<List<DatabaseEntities.Local.Exception>> GetTestCaseException(int id);
        Task RemoveTestCaseException(int id);
    }
}
