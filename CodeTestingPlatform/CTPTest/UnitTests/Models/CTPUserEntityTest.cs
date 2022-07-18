using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CTPTest.Helpers;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Models {
    public class CTPUserEntityTest {
        private readonly TestSetup _testSetup = new();

        public CTPUserEntityTest() {
        }
        /*
        [Fact]
        public async Task TestCTPUser_GetUser_Exists() {
            using var context = _testSetup.CreateContext();
            Ctpuser _user = new(context);

            var user = await _user.GetUser(22);

            Assert.Equal("Ronald", user.FirstName);
            Assert.Equal("Patterson", user.LastName);
        }

        [Fact]
        public async Task TestCTPUser_GetUser_DoesNot_Exist() {
            using var context = _testSetup.CreateContext();
            Ctpuser _user = new(context);

            var user = await _user.GetUser(99);

            Assert.Null(user);
        }

        [Fact]
        public async Task TestCTPUser_CreateUser_Teacher() {
            using var context = _testSetup.CreateContext();
            Ctpuser _user = new(context);
            Teacher _teacher = new(context);

            await _user.CreateNewUser("Test", "Test", 42069, true);

            Assert.NotNull(await _teacher.GetTeacher(42069));
        }

        [Fact]
        public async Task TestCTPUser_CreateUser_Student() {
            using var context = _testSetup.CreateContext();
            Ctpuser _user = new(context);
            Student _student = new(context);

            List<Ctpuser> userList = context.Ctpusers.ToList();

            await _user.CreateNewUser("Test", "Test", 42069, false);

            Assert.NotNull(await _student.GetStudent(42069));
        }
        */
    }
}
