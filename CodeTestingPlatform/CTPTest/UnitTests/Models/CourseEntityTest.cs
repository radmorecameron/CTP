using CodeTestingPlatform.DatabaseEntities.Local;
using CTPTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Models {
    public class CourseEntityTest {
        public readonly TestSetup ts;
        public CourseEntityTest() {
            ts = new();
        }
        /*
        [Fact]
        public async Task TestActivityType_GetCourse_Exists() {
            using var context = ts.CreateContext();
            Course _course = new(context);
            Course course = await _course.GetCourseById(1);
            Assert.NotNull(course);
        }
        [Fact]
        public async Task TestActivityType_GetCourse_DoesNot_Exist() {
            using var context = ts.CreateContext();
            Course _course = new(context);
            Course course = await _course.GetCourseById(-1);
            Assert.Null(course);
        }
        [Fact]
        public async Task TestCourse_IsNewCourse_False() {
            using var context = ts.CreateContext();
            Course c = new (1,"Programming III","420-G30", context);
            bool result = await c.IsNewCourse();
            Assert.False(result);
        }
        [Fact]
        public async Task TestCourse_IsNewCourse_True() {
            using var context = ts.CreateContext();
            Course _course = new(context);
            Course c = new(99, "Programming XI", "420-G90", context);
            bool result = await c.IsNewCourse();
            Assert.True(result);
        }
        [Fact]
        public async Task TestCourse_GetCourseByClaraCode() {
            using var context = ts.CreateContext();
            Course _course = new(context);
            Course result = await _course.GetCourseByClaraCode("420G30");
            Assert.NotNull(result);
        }
        [Fact]
        public async Task TestCourse_GetCourseName () {
            using var context = ts.CreateContext();
            Course _course = new(context);
            string result = await _course.GetCourseName(1);
            Assert.Equal("Programming III", result);
        }
        [Fact]
        public async Task TestCourse_Create_Success() {
            using var context = ts.CreateContext();
            Course _course = new(context) {
                CourseId = 98,
                CourseName = "Prog",
                CourseCode = "420-H93"
            };
            try {
                await _course.Create();
                Assert.True(true);
            } catch (Exception) {
                Assert.True(false);
            }
        }
        [Fact]
        public async Task TestCourse_Create_Fail() {
            using var context = ts.CreateContext();
            Course _course = new(context) {
                CourseId = 1,
                CourseName = "Prog",
                CourseCode = "420-H93"
            };
            try {
                await _course.Create();
                Assert.True(false);
            } catch (Exception) {
                Assert.True(true);
            }
        }
        [Fact]
        public async Task TestCourse_Edit() {
            using var context = ts.CreateContext();
            Course _course = new(context);
            Course course = await _course.GetCourseById(1);
            course.CourseName = "Programming 3";
            await course.EditCourse();
            Course result = await course.GetCourseById(1);
            Assert.Equal("Programming 3", result.CourseName);
            Assert.Equal(1, result.CourseId);
            Assert.Equal("420-G30", result.CourseCode);
        }
        [Fact]
        public async Task TestCourse_Remove() {
            using var context = ts.CreateContext();
            Course _course = new(context) {
                CourseId = 98,
                CourseName = "Prog",
                CourseCode = "420-H93"
            };
            await _course.Create();
            await _course.RemoveCourse();
            Course result = await _course.GetCourseById(98);
            Assert.Null(result);
        }
        [Fact]
        public async Task TestCourse_GetCourseNames() {
            CTP_TESTContext context = ts.CreateContext();
            Course _course = new(context);
            List<Course> courses = await _course.GetCourseNames();
            Assert.NotEmpty(courses);
        }
        */
    }
}
