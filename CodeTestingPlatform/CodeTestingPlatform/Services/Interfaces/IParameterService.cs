using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services.Interfaces {
    public interface IParameterService {
        Task<Parameter> FindByIdAsync(int id);
        Task<List<Parameter>> ListAsync();
        Task UpdateAsync(Parameter param);
        Task CreateAsync(Parameter param);
        Task DeleteAsync(Parameter param);
        void DetachEntities();
    }
}
