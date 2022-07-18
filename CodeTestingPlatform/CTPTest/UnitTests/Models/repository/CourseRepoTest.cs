using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories;
using CodeTestingPlatform.Services;
using CTPTest.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Models.repository {
    [ExcludeFromCodeCoverage]
    public class CourseRepoTest {
        public readonly TestSetup ts;
        public CourseRepoTest() {
            ts = new();
        }
        public CourseService GetCourseService(CTP_TESTContext context) {
            CourseRepository cr = new(context);
            UserRepository ur = new(context);
            StudentRepository sr = new(context);
            TeacherRepository tr = new(context);
            ClaraRepository clr = new(context);
            return new(cr, ur, sr, tr, clr);
        }
        [Fact]
        public async Task Course_IsUserInCourse() {
            using var context = ts.CreateContext();
            CourseRepository _courseRepo = new(context);
            Assert.True(await _courseRepo.IsUserInCourse(31, 20));
        }
        [Fact]
        public async Task Course_GetCoursesByUserId() {
            using var context = ts.CreateContext();
            CourseRepository _courseRepo = new(context);
            Assert.NotEmpty(await _courseRepo.GetCoursesByUserId(31));
        }
        [Fact]
        public async Task Course_GetCourseNameByIdAsync() {
            using var context = ts.CreateContext();
            CourseRepository _courseRepo = new(context);
            Assert.Equal("Programming III", await _courseRepo.GetCourseNameByIdAsync(1));
        }
        [Fact]
        public async Task Course_GetCourseNameByIdAsync_InvalidId() {
            using var context = ts.CreateContext();
            CourseRepository _courseRepo = new(context);
            Assert.Null(await _courseRepo.GetCourseNameByIdAsync(-1));
        }
        [Fact]
        public async Task Course_IsNewCourse_False() {
            using var context = ts.CreateContext();
            CourseRepository _courseRepo = new(context);
            bool result = await _courseRepo.IsNewCourseAsync("420-G30");
            Assert.False(result);
        }
        [Fact]
        public async Task Course_IsNewCourse_True() {
            using var context = ts.CreateContext();
            CourseRepository _courseRepo = new(context);
            bool result = await _courseRepo.IsNewCourseAsync("420-G90");
            Assert.True(result);
        }
        [Fact]
        public async Task Course_AddUserCourse() {
            using var context = ts.CreateContext();
            CourseRepository _courseRepo = new(context);
            try {
                await _courseRepo.AddUserCourse(31, 1);
                Assert.True(true);
            } catch(System.Exception) {
                Assert.False(true);
            }
        }
        [Fact]
        public async Task Course_RemoveUserCourse() {
            using var context = ts.CreateContext();
            CourseRepository _courseRepo = new(context);
            try {
                var uCourse = (await _courseRepo.GetCoursesByUserId(31)).First();
                await _courseRepo.RemoveUserCourse(uCourse);
                Assert.True(true);
            } catch (System.Exception) {
                Assert.False(true);
            }
        }
        [Fact]
        public async Task Course_CreateCourse() {
            using var context = ts.CreateContext();
            CourseRepository _courseRepo = new(context);
            List<Course> courseList = new();
            try {
                Course c = new() {
                    CourseCode = "420-H80",
                    CourseName = "Advanced Hardware & Operating Systems",
                };
                courseList.Add(c);
                await _courseRepo.CreateAsync(c);
                await _courseRepo.AddClaraCourses(courseList);
                Assert.True(true);
            } catch (System.Exception) {
                Assert.False(true);
            }
        }
        [Fact]
        public async Task Course_DeleteCourse() {
            using var context = ts.CreateContext();
            CourseRepository _courseRepo = new(context);
            Course c = await _courseRepo.FindByIdAsync(1);
            try {
                await _courseRepo.DeleteAsync(c);
                Assert.True(true);
            } catch (System.Exception) {
                Assert.False(true);
            }
        }
        [Fact]
        public async Task Course_UpdateCourse() {
            using var context = ts.CreateContext();
            CourseRepository _courseRepo = new(context);
            Course c = await _courseRepo.FindByIdAsync(1);
            c.CourseName = "Fortnite 101";
            try {
                await _courseRepo.UpdateAsync(c);
                Assert.True(true);
            } catch (System.Exception) {
                Assert.False(true);
            }
        }
        [Fact]
        public async Task Course_GetCourseNamesAsync() {
            using var context = ts.CreateContext();
            CourseRepository _courseRepo = new(context);
            List<Course> c = await _courseRepo.GetCourseNamesAsync();
            Assert.NotEmpty(c);
        }
        [Fact]
        public async Task Course_ExistsAsync() {
            using var context = ts.CreateContext();
            CourseRepository _courseRepo = new(context);
            bool result = await _courseRepo.ExistsAsync(1);
            Assert.True(result);
        }
        [Fact]
        public async Task Course_ListAsync() {
            using var context = ts.CreateContext();
            CourseRepository _courseRepo = new(context);
            List<UserCourse> result = await _courseRepo.ListAsync(31);
            Assert.NotEmpty(result);
        }
    }
}
