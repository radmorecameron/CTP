using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories {
    public class DataTypeRepository : GenericRepository<DataType>, IDataTypeRepository {

        public DataTypeRepository(CTP_TESTContext context) : base(context) {

        }
        public async Task<List<DataType>> ListAsync(int? LanguageId = null) {
            IQueryable<DataType> results = _context.DataTypes;

            if (LanguageId != null)
                results = results.Where(t => t.LanguageId == LanguageId);

            return await results.OrderBy(t => t.DataType1).ToListAsync();
        }

        public async Task<DataType> FindByIdAsync(int id) {
            return await _context.DataTypes.Where(t => t.DataTypeId == id).FirstOrDefaultAsync();
        }
        public async Task<bool> ExistsAsync(int id) {
            return await _context.DataTypes.AnyAsync(e => e.DataTypeId == id);
        }
    }
}
