using CodeTestingPlatform.DatabaseEntities.Local;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories {
    public class ParameterRepository : GenericRepository<Parameter>, IParameterRepository {
        public ParameterRepository(CTP_TESTContext context) : base(context) {

        }
        public async Task<List<Parameter>> ListAsync() {
            return await _context.Parameters
                .AsNoTracking()
                .Include(p => p.TestCase)
                .Include(p => p.SignatureParameter)
                .ToListAsync();
        }

        public async Task<Parameter> FindByIdAsync(int id) {
            return await _context.Parameters.AsNoTracking()
                                              .Include(p => p.TestCase)
                                              .AsNoTracking()
                                              .Include(p => p.SignatureParameter)
                                              .AsNoTracking()
                                              .SingleOrDefaultAsync(t => t.ParameterId == id);
        }

        public async Task UpdateAsync(Parameter param) {
            _context.Parameters.Update(param);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(Parameter param) {
            _context.Parameters.Add(param);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Parameter param) {
            _context.Parameters.Remove(param);
            await _context.SaveChangesAsync();
        }

        public void DetachEntities() {
            _context.ChangeTracker.Clear();
        }
    }
}
