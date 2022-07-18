using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories {
    public class LanguageRepository : GenericRepository<Language>, ILanguageRepository {
        public LanguageRepository(CTP_TESTContext context) : base(context) {

        }
        public async Task<List<Language>> ListAsync() {
            return await _context.Languages
                .OrderBy(l => l.LanguageName)
                .Select(lang => new Language { LanguageId = lang.LanguageId, LanguageName = lang.LanguageFullName })
                .ToListAsync();
        }
        public async Task<Language> FindByIdAsync(int id) {
            return await _context.Languages.SingleOrDefaultAsync(l => l.LanguageId == id);
        }
        public async Task<bool> ExistsAsync(int id) {
            return await _context.Languages.AnyAsync(e => e.LanguageId == id);
        }

       
    }
}
