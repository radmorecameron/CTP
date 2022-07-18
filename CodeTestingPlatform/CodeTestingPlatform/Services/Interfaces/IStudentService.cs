using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services.Interfaces {
    public interface IStudentService {
        Task<Student> FindByIdAsync(int id);
        Task CreateAsync(Student student);
        Task<bool> ExistsAsync(int id);
        Task<bool> IsNewStudentAsync(int studentId);
        Task<List<Ctpuser>> GetStudentsFromCourse(int courseId);
    }
}
