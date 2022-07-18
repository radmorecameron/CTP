using CodeTestingPlatform.DatabaseEntities.Local;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories.Interfaces {
    public interface IStudentRepository {
        Task<Student> FindByIdAsync(int id);
        Task CreateAsync(Student student);
        Task<bool> ExistsAsync(int id);
        Task<List<Ctpuser>> GetStudentsFromCourse(int courseId);
    }
}
