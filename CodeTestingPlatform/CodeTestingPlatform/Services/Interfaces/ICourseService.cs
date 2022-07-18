using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services.Interfaces {
    public interface ICourseService {
        Task<Course> FindByIdAsync(int id);
        Task<Course> FindByCodeAsync(string courseCode);

        Task<List<Course>> ListAsync(int userId, int semesterId, bool isTeacher);

        Task CreateAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(Course course);
        Task<bool> ExistsAsync(int id);

        Task AddClaraCoursesAsync(List<Course> courseList);
        Task<bool> IsNewCourseAsync(string courseCode);
        Task<List<Course>> GetCourseNamesAsync();
        Task<string> GetCourseNameByIdAsync(int id);

        Task AddUserCoursesAsync(Ctpuser user, List<Course> claraCourses);
        Task AddStudentCoursesAsync(Student student, int semesterId);
        Task AddTeacherCoursesAsync(Teacher teacher, int semesterId);
        Task<List<UserCourse>> ListAsync(int userId);
        Task<Course> FindStudentCourse(int id);
        Dictionary<int, List<string>> FindInvalidTestCases(IEnumerable<Activity> activities, out Dictionary<int, int> invalidActivities, out Dictionary<int, int> invalidSignatures);
    }
}
