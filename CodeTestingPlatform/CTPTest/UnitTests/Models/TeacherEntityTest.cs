using CodeTestingPlatform.DatabaseEntities.Local;
using CTPTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Models {
    public class TeacherEntityTest {
        public readonly TestSetup ts;
        public TeacherEntityTest() {
            ts = new();
        }
        /*
        [Fact]
        public async Task TestStudent_GetStudent_Exists() {
            using var context = ts.CreateContext();
            Teacher _teacher = new(context);
            Teacher teacher = await _teacher.GetTeacher(2876);
            Assert.Equal(2876, teacher.TeacherId);
            Assert.Equal(31, teacher.UserId);
            Assert.False(await teacher.IsNewTeacher());
        }
        [Fact]
        public async Task TestStudent_GetStudent_Doesnt_Exist() {
            using var context = ts.CreateContext();
            Student _student = new(context);
            Student student = await _student.GetStudent(1);
            Assert.Null(student);
        }
        [Fact]
        public async Task TestStudent_Create_Success() {
            using var context = ts.CreateContext();
            Student _student = new(context) {
                StudentId = 911,
                UserId = 36
            };
            try {
                await _student.Create();
                Assert.True(true);
            } catch (Exception) {
                Assert.True(false);
            }
        }
        [Fact]
        public async Task TestStudent_Create_Fail() {
            using var context = ts.CreateContext();
            Student _student = new(context) {
                StudentId = 24472,
                UserId = 36
            };
            try {
                await _student.Create();
                Assert.True(false);
            } catch (Exception) {
                Assert.True(true);
            }
        }
        [Fact]
        public async Task TestStudent_IsNewStudent_False() {
            using var context = ts.CreateContext();
            Student _student = new(context) {
                StudentId = 24472,
                UserId = 36
            };
            Assert.False(await _student.IsNewStudent());
        }
        [Fact]
        public async Task TestStudent_IsNewStudent_True() {
            using var context = ts.CreateContext();
            Student _student = new(context) {
                StudentId = 911,
                UserId = 36
            };
            Assert.True(await _student.IsNewStudent());
        }
        */
    }
}
