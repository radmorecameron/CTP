using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public class ExceptionService : IExceptionService {
        private readonly IExceptionRepository _exceptionRepository;
        public ExceptionService(IExceptionRepository exceptionRepository) {
            _exceptionRepository = exceptionRepository;
        }

        public async Task<DatabaseEntities.Local.Exception> FindByIdAsync(int id) {
            return await _exceptionRepository.FindByIdAsync(id);
        }

        public async Task<IList<DatabaseEntities.Local.Exception>> ListAsync(int languageId) {
            return await _exceptionRepository.ListAsync(languageId);
        }

        public async Task<bool> ExistsAsync(int id) {
            return await _exceptionRepository.ExistsAsync(id);
        }

        public async Task AddSignatureException(SignatureException signatureExpection) {
            await _exceptionRepository.AddSignatureException(signatureExpection);
        }

        public async Task<List<DatabaseEntities.Local.Exception>> GetSignatureExceptions(int id) {
            return await _exceptionRepository.GetSignatureExceptions(id);
        }

        public async Task RemoveSignatureExceptions(int id) {
            await _exceptionRepository.RemoveSignatureExceptions(id);
        }

        public async Task AddTestCaseException(TestCaseException testCaseExpection) {
            await _exceptionRepository.AddTestCaseException(testCaseExpection);
        }

        public async Task<List<DatabaseEntities.Local.Exception>> GetTestCaseException(int id) {
            return await _exceptionRepository.GetTestCaseException(id);
        }

        public async Task RemoveTestCaseException(int id) {
            await _exceptionRepository.RemoveTestCaseException(id);
        }
    }
}
