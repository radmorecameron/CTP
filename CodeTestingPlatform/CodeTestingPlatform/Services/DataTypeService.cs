using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public class DataTypeService : IDataTypeService {
        private readonly IDataTypeRepository _dataTypeRepository;
        public DataTypeService(IDataTypeRepository dataTypeRepository) {
            _dataTypeRepository = dataTypeRepository;
        }

        public async Task<DataType> FindByIdAsync(int id) {
            return await _dataTypeRepository.FindOneAsync(d => d.DataTypeId == id);
        }

        public async Task<IList<DataType>> ListAsync(int languageId) {
            return await _dataTypeRepository.GetAllAsync(predicate: t => t.LanguageId == languageId);
        }

        public async Task<bool> ExistsAsync(int id) {
            return await _dataTypeRepository.ExistsAsync(t => t.DataTypeId == id);
        }
    }
}
