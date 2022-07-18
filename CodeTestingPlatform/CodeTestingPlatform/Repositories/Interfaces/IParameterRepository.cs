using CodeTestingPlatform.DatabaseEntities.Local;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories {
    public interface IParameterRepository {
        Task<Parameter> FindByIdAsync(int id);
        Task<List<Parameter>> ListAsync();
        Task UpdateAsync(Parameter param);
        Task CreateAsync(Parameter param);
        Task DeleteAsync(Parameter param);
        void DetachEntities();
    }
}