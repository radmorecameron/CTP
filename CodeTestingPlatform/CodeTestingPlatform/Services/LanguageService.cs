using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public class LanguageService : ILanguageService {
        private readonly ILanguageRepository _languageRepository;
        public LanguageService(ILanguageRepository languageRepository) {
            _languageRepository = languageRepository;
        }

        public async Task<Language> FindByIdAsync(int id) {
            return await _languageRepository.FindByIdAsync(id);
        }

        public async Task<List<Language>> ListAsync() {
            return await _languageRepository.ListAsync();
        }

        public async Task<bool> ExistsAsync(int id) {
            return await _languageRepository.ExistsAsync(id);
        }
    }
}
