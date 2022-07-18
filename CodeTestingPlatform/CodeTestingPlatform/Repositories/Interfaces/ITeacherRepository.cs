using CodeTestingPlatform.DatabaseEntities.Local;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories.Interfaces {
    public interface ITeacherRepository {
        Task<Teacher> FindByIdAsync(int id);
        //Task<List<Teacher>> ListAsync();
        Task CreateAsync(Teacher teacher);
        //Task UpdateAsync(Teacher teacher);
        //Task DeleteAsync(Teacher teacher);
        Task<bool> IsNewTeacherAsync(int teacherId);
    }
}
