using CodeTestingPlatform.DatabaseEntities.Local;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories.Interfaces {
    public interface ICourseRepository {
        Task<Course> FindByIdAsync(int id);
        Task<Course> FindByCodeAsync(string courseCode);
        //Task<List<Course>> ListAsync(int userId, int semesterId, bool isTeacher);
        Task<List<UserCourse>> ListAsync(int userId);
        Task CreateAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(Course course);
        Task<bool> ExistsAsync(int id);

        Task AddClaraCourses(List<Course> courseList);
        Task<bool> IsNewCourseAsync(string courseCode);
        Task<List<Course>> GetCourseNamesAsync();
        Task<string> GetCourseNameByIdAsync(int id);
        Task AddUserCourse(int userId, int courseId);
        Task<bool> IsUserInCourse(int userId, int courseId);
        Task RemoveUserCourse(UserCourse userCourse);
        Task<Course> FindStudentCourse(int id);
    }
}
