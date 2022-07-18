using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services.Interfaces {
    public interface IDataTypeService {
        Task<DataType> FindByIdAsync(int id);
        Task<IList<DataType>> ListAsync(int languageId);
        Task<bool> ExistsAsync(int id);
    }
}
