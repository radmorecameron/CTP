using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public class UserDefinedExceptionService : IUserDefinedExceptionService {
        private readonly IUserDefinedExceptionRepository _userDefinedExceptionRepository;
        public UserDefinedExceptionService(IUserDefinedExceptionRepository userDefinedExceptionRepository) {
            _userDefinedExceptionRepository = userDefinedExceptionRepository;
        }

        public async Task<UserDefinedException> FindByIdAsync(int id) {
            return await _userDefinedExceptionRepository.FindByIdAsync(id);
        }

        public async Task<List<UserDefinedException>> ListAsync(int languageId) {
            return await _userDefinedExceptionRepository.ListAsync(languageId);
        }

        public async Task<bool> ExistsAsync(int id) {
            return await _userDefinedExceptionRepository.ExistsAsync(id);
        }

        public async Task AddSignatureUserDefinedException(SignatureUserDefinedException signatureExpection) {
            await _userDefinedExceptionRepository.AddSignatureUserDefinedException(signatureExpection);
        }

        public async Task<int> AddUserDefinedExceptionByName(string name, int languageId) {
            return await _userDefinedExceptionRepository.AddUserDefinedExceptionByName(name, languageId);
        }

        public async Task<List<UserDefinedException>> GetSignatureUserDefinedExceptions(int id) {
            return await _userDefinedExceptionRepository.GetSignatureUserDefinedExceptions(id);
        }

        public async Task RemoveSignatureUserDefinedExceptions(int id) {
            await _userDefinedExceptionRepository.RemoveSignatureUserDefinedExceptions(id);
        }

        public async Task AddUserDefinedException(UserDefinedException userDefinedException) {
            await _userDefinedExceptionRepository.AddUserDefinedException(userDefinedException);
        }
    }
}
