using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories.Interfaces {
    public interface ILanguageRepository {
        Task<Language> FindByIdAsync(int id);
        Task<List<Language>> ListAsync();
        Task<bool> ExistsAsync(int id);
        //Task CreateAsync(Language language);
        //Task UpdateAsync(Language language);
        //Task DeleteAsync(Language language);
    }
}
