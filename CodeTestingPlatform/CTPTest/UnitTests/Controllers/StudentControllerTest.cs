using CodeTestingPlatform.Controllers;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Repositories;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services;
using CodeTestingPlatform.Services.Interfaces;
using CTPTest.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Controllers {
    public class StudentControllerTest {
        public readonly TestSetup ts;
        public StudentControllerTest() {
            ts = new();
        }
        public StudentController CreateController(ICurrentSession session, CTP_TESTContext ctx = null) {
            if (ctx == null) {
                ctx = ts.CreateContext();
            }
            #region repositories
            ICourseRepository icr = new CourseRepository(ctx);
            IUserRepository iur = new UserRepository(ctx);
            IStudentRepository isr = new StudentRepository(ctx);
            ITeacherRepository itr = new TeacherRepository(ctx);
            IClaraRepository iclarar = new ClaraRepository(ctx);
            #endregion
            #region services
            ICourseService cs = new CourseService(icr, iur, isr, itr, iclarar);
            IStudentService iss = new StudentService(isr);
            #endregion
            StudentController pc = new(session, iss, cs);
            pc.TempData = new Mock<ITempDataDictionary>().Object;
            return pc;
        }
        [Fact]
        public async Task IndexReturnsMainView() {
            // Arrange
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserATeacher()).Returns(true);
            mockSession.Setup(session => session.GetStudentId()).Returns(24472);
            StudentController mockController = CreateController(mockSession.Object);
            // Act
            IActionResult result = await mockController.Index();
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
    }
}
