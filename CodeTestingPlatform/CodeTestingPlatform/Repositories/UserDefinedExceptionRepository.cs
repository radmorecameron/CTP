using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories {
    public class UserDefinedExceptionRepository : GenericRepository<UserDefinedException>, IUserDefinedExceptionRepository{
        public UserDefinedExceptionRepository(CTP_TESTContext context) : base(context) {
        }

        public async Task<List<UserDefinedException>> ListAsync(int? LanguageId = null) {
            IQueryable<UserDefinedException> results = _context.UserDefinedExceptions;

            if (LanguageId != null)
                results = results.Where(t => t.LanguageId == LanguageId);

            return await results.OrderBy(t => t.UserDefinedExceptionName).ToListAsync();
        }

        public async Task<UserDefinedException> FindByIdAsync(int id) {
            return await _context.UserDefinedExceptions.Where(t => t.UserDefinedExceptionId == id).FirstOrDefaultAsync();
        }
        public async Task<bool> ExistsAsync(int id) {
            return await _context.UserDefinedExceptions.AnyAsync(e => e.UserDefinedExceptionId == id);
        }

        public async Task AddSignatureUserDefinedException(SignatureUserDefinedException signatureExpection) {
            _context.SignatureUserDefinedExceptions.Add(signatureExpection);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserDefinedException>> GetSignatureUserDefinedExceptions(int id) {
            return await _context.SignatureUserDefinedExceptions.Include(s => s.UserDefinedException)
                .Where(s => s.SignatureId == id)
                .Select(x => x.UserDefinedException)
                .ToListAsync();
        }

        public async Task RemoveSignatureUserDefinedExceptions(int id) {
            var signatureUserDefinedExceptions = await _context.SignatureUserDefinedExceptions.Where(s => s.SignatureId == id).ToListAsync();
            foreach (var sigExcept in signatureUserDefinedExceptions) {
                _context.SignatureUserDefinedExceptions.Remove(sigExcept);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddUserDefinedExceptionByName(string name, int languageId) {
            var userDefinedException = await _context.UserDefinedExceptions.SingleOrDefaultAsync(x => x.UserDefinedExceptionName == name);

            if(userDefinedException == null) {
                _context.UserDefinedExceptions.Add(new UserDefinedException { UserDefinedExceptionName = name, LanguageId = languageId});
                await _context.SaveChangesAsync();
                userDefinedException = await _context.UserDefinedExceptions.Where(x => x.UserDefinedExceptionName == name).SingleOrDefaultAsync();
            }

            return userDefinedException.UserDefinedExceptionId;
        }

        public async Task AddUserDefinedException(UserDefinedException userDefinedException) {
            await _context.UserDefinedExceptions.AddAsync(userDefinedException);
            await _context.SaveChangesAsync();
        }
    }
}
