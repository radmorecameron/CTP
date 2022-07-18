using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories {
    public class ExceptionRepository : GenericRepository<DatabaseEntities.Local.Exception>, IExceptionRepository {

        public ExceptionRepository(CTP_TESTContext context) : base(context) {

        }

        public async Task<List<DatabaseEntities.Local.Exception>> ListAsync(int? LanguageId = null) {
            IQueryable<DatabaseEntities.Local.Exception> results = _context.Exceptions;

            if (LanguageId != null)
                results = results.Where(t => t.LanguageId == LanguageId);

            return await results.OrderBy(t => t.ExceptionName).ToListAsync();
        }

        public async Task<DatabaseEntities.Local.Exception> FindByIdAsync(int id) {
            return await _context.Exceptions.Where(t => t.ExceptionId == id).FirstOrDefaultAsync();
        }
        public async Task<bool> ExistsAsync(int id) {
            return await _context.Exceptions.AnyAsync(e => e.ExceptionId == id);
        }

        public async Task AddSignatureException(SignatureException signatureExpection) {
            _context.SignatureExceptions.Add(signatureExpection);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DatabaseEntities.Local.Exception>> GetSignatureExceptions(int id) {
            return await _context.SignatureExceptions.Include(s => s.Exception).Where(s => s.SignatureId == id).Select(s => s.Exception).ToListAsync();
        }

        public async Task RemoveSignatureExceptions(int id) {
            var signatureExceptions = await _context.SignatureExceptions.Where(s => s.SignatureId == id).ToListAsync();
            foreach (var sigExcept in signatureExceptions) {
                _context.SignatureExceptions.Remove(sigExcept);
            }
            await _context.SaveChangesAsync();
        }

        public async Task AddTestCaseException(TestCaseException testCaseExpection) {
            _context.TestCaseExceptions.Add(testCaseExpection);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DatabaseEntities.Local.Exception>> GetTestCaseException(int id) {
            return await _context.TestCaseExceptions.Include(s => s.Exception).Where(s => s.TestCaseId == id).Select(s => s.Exception).ToListAsync();
        }

        public async Task RemoveTestCaseException(int id) {
            var testCaseException = await _context.TestCaseExceptions.Where(s => s.TestCaseId == id).FirstOrDefaultAsync();
            if (testCaseException != null) {
                _context.TestCaseExceptions.Remove(testCaseException);
            }
            await _context.SaveChangesAsync();
        }
    }
}
