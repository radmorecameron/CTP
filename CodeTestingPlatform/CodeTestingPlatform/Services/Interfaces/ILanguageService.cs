using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services.Interfaces {
    public interface ILanguageService {
        Task<Language> FindByIdAsync(int id);
        Task<List<Language>> ListAsync();
        Task<bool> ExistsAsync(int id);
    }
}
