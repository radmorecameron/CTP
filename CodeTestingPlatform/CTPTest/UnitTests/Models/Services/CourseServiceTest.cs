using CodeTestingPlatform.DatabaseEntities.Clara;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Repositories;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services;
using CodeTestingPlatform.Services.Interfaces;
using CTPTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Models.Services {
    public class CourseServiceTest {
        private readonly TestSetup ts;
        public CourseServiceTest() {
            ts = new();
        }
        public ICourseService CreateCourseService() {
            CTP_TESTContext ctx = ts.CreateContext();
            ICourseRepository cr = new CourseRepository(ctx); 
            IUserRepository ur = new UserRepository(ctx); 
            ITeacherRepository tr = new TeacherRepository(ctx); 
            IStudentRepository sr = new StudentRepository(ctx); 
            IClaraRepository clr = new ClaraRepository(ctx);
            ICourseService ics = new CourseService(cr, ur, sr, tr, clr);
            return ics;
        }
        [Fact]
        public async Task FindByIdAsync() {
            ICourseService cService = CreateCourseService();
            Course course = await cService.FindByIdAsync(1);
            Assert.NotNull(course);
        }
        [Fact]
        public async Task FindByCodeAsync() {
            ICourseService cService = CreateCourseService();
            Course course = await cService.FindByCodeAsync("420-G30");
            Assert.NotNull(course);
        }
        [Fact]
        public async Task ListAsync() {
            ICourseService cService = CreateCourseService();
            List<UserCourse> course = await cService.ListAsync(31);
            Assert.NotNull(course);
        }
        [Fact]
        public async Task AddClaraCoursesAsync() {
            ICourseService cService = CreateCourseService();
            List<Course> courses = new() {
                new Course { CourseId = 99, CourseCode = "420-KSD", CourseName = "Advanced Topic 3" },
                new Course { CourseId = 102, CourseCode = "420-K90", CourseName = "Basic Topic 1" }
            };
            await cService.AddClaraCoursesAsync(courses);
            Assert.True(true);
        }
    }
}
