using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CodeTestingPlatform.Repositories {
    public class CourseRepository : GenericRepository<Course>, ICourseRepository {
        public CourseRepository(CTP_TESTContext context) : base(context) {

        }

        //[ExcludeFromCodeCoverageAttribute]
        //public async Task<List<Course>> ListAsync(int userId, int semesterId, bool isTeacher) {
        //    List<Course> courseList = new();
        //    string userType = (isTeacher && isTeacher) ? "Teacher" : "Student";
        //    SqlParameter userIdParam = new() {
        //        ParameterName = $"{userType}ID",
        //        Value = userId
        //    };
        //    SqlParameter semesterParam = new() {
        //        ParameterName = "Semester",
        //        Value = semesterId
        //    };
        //    courseList = await _dbContext.Courses.FromSqlRaw(@$"
        //        SelectCoursesFor{userType} @{userType}ID, @Semester",
        //        parameters: new[] { userIdParam, semesterParam }).ToListAsync();

        //    await AddClaraCourses(courseList);
        //    return courseList;
        //}

        public async Task<List<UserCourse>> ListAsync(int userId) {
            return await _context.UserCourses.Include(c => c.Course)
                .ThenInclude(a => a.Activities)
                .Where(u => u.UserId == userId)
                .OrderBy(a => a.Course.CourseName)
                .ThenBy(b => b.CourseId)
                .ToListAsync();
        }
        public async Task AddClaraCourses(List<Course> courseList) {
            if (courseList.Count > 0) {
                foreach (Course course in courseList) {
                    if (await IsNewCourseAsync(course.CourseCode)) {
                        await CreateAsync(course);
                    }
                }
            }
        }

        public async Task<Course> FindByIdAsync(int id) {
            return await _context.Courses.Include(a => a.Activities)
                                        .ThenInclude(l => l.Language)
                                    .Include(a => a.Activities)
                                        .ThenInclude(a => a.ActivityType)
                                    .Include(a => a.Activities)
                                        .ThenInclude(a => a.MethodSignatures)
                                            .ThenInclude(a => a.TestCases)
                                                .ThenInclude(s => s.Parameters.OrderBy(s => s.SignatureParameter.ParameterPosition))
                                                    .ThenInclude(a => a.SignatureParameter)
                                                        .ThenInclude(p => p.DataType)
                                    .Include(a => a.Activities)
                                        .ThenInclude(a => a.MethodSignatures)
                                            .ThenInclude(a => a.SignatureParameters.OrderBy(s => s.ParameterPosition))
                                                .ThenInclude(a => a.DataType)
                                    .Include(a => a.Activities)
                                        .ThenInclude(a => a.MethodSignatures)
                                            .ThenInclude(a => a.ReturnType)
                                    .Include(s => s.Activities)
                                        .ThenInclude(s => s.MethodSignatures)
                                            .ThenInclude(s => s.TestCases)
                                                .ThenInclude(e => e.TestCaseException)
                                                    .ThenInclude(e => e.Exception)
                                    .FirstOrDefaultAsync(x => x.CourseId == id);
        }

        public async Task<Course> FindStudentCourse(int id) {
            return await _context.Courses.Include(a => a.Activities.Where(a => (a.EndDate >= System.DateTime.Today || a.EndDate == null) && a.StartDate <= System.DateTime.Today).OrderBy(a => a.StartDate))
                                        .ThenInclude(l => l.Language)
                                    .Include(a => a.Activities.Where(a => (a.EndDate >= System.DateTime.Today || a.EndDate == null) && a.StartDate <= System.DateTime.Today).OrderBy(a => a.StartDate))
                                        .ThenInclude(a => a.ActivityType)
                                    .Include(a => a.Activities.Where(a => (a.EndDate >= System.DateTime.Today || a.EndDate == null) && a.StartDate <= System.DateTime.Today).OrderBy(a => a.StartDate))
                                        .ThenInclude(a => a.MethodSignatures)
                                            .ThenInclude(a => a.TestCases)
                                                .ThenInclude(s => s.Parameters.OrderBy(s => s.SignatureParameter.ParameterPosition))
                                                    .ThenInclude(a => a.SignatureParameter)
                                                        .ThenInclude(p => p.DataType)
                                    .Include(a => a.Activities.Where(a => (a.EndDate >= System.DateTime.Today || a.EndDate == null) && a.StartDate <= System.DateTime.Today).OrderBy(a => a.StartDate))
                                        .ThenInclude(a => a.MethodSignatures)
                                            .ThenInclude(a => a.SignatureParameters.OrderBy(s => s.ParameterPosition))
                                                .ThenInclude(a => a.DataType)
                                    .Include(a => a.Activities.Where(a => (a.EndDate >= System.DateTime.Today || a.EndDate == null) && a.StartDate <= System.DateTime.Today).OrderBy(a => a.StartDate))
                                        .ThenInclude(a => a.MethodSignatures)
                                            .ThenInclude(a => a.ReturnType)
                                    .FirstOrDefaultAsync(x => x.CourseId == id);
        }

        public async Task<Course> FindByCodeAsync(string courseCode) {
            courseCode = !courseCode.Contains('-') ? $"{courseCode.Substring(0, 3)}-{courseCode.Substring(3, 3)}" : courseCode;
            Course course = await _context.Courses
                        .FirstOrDefaultAsync(c => c.CourseCode == courseCode);
            return course;
        }

        public async Task CreateAsync(Course course) {
            course.CourseId = 0;
            course.CourseCode = course.CourseCode.Substring(0, 3) + "-" + course.CourseCode.Substring(3, 3);
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public override async Task DeleteAsync(Course course) {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course) {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsNewCourseAsync(string courseCode) {
            return (await FindByCodeAsync(courseCode)) == null;
        }

        public async Task<List<Course>> GetCourseNamesAsync() {
            //For dropdown list
            return await _context.Courses
                            .Select(c => new Course { CourseId = c.CourseId, CourseName = c.CourseName })
                            .ToListAsync();
        }

        public async Task<string> GetCourseNameByIdAsync(int id) {
            return (await _context.Courses
                    .FirstOrDefaultAsync(c => c.CourseId == id))?.CourseName;
        }

        public async Task<bool> ExistsAsync(int id) {
            return await _context.Courses.AnyAsync(e => e.CourseId == id);
        }

        public async Task RemoveUserCourse(UserCourse userCourse) {
            _context.UserCourses.Remove(userCourse);
            await _context.SaveChangesAsync();
        }

        public async Task AddUserCourse(int userId, int courseId) {
            _context.UserCourses.Add(new UserCourse() { UserId = userId, CourseId=courseId });
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUserInCourse(int userId, int courseId) {
            UserCourse userCourse = await _context.UserCourses
                            .FirstOrDefaultAsync(c => c.UserId == userId && c.CourseId == courseId);
            return userCourse != null;
        }

        public async Task<IEnumerable<UserCourse>> GetCoursesByUserId(int userId) {
            return await _context.UserCourses.Include(c => c.Course)
                .Where(u => u.UserId == userId)
                .OrderBy(a => a.Course.CourseName)
                .ThenBy(b => b.CourseId)
                .ToListAsync();
        }
    }
}
