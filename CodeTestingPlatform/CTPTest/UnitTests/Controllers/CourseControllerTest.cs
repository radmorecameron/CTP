using CodeTestingPlatform.Controllers;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Repositories;
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
    public class CourseControllerTest {
        public readonly TestSetup ts;
        public CourseControllerTest() {
            ts = new();
        }
        public CourseController CreateController(ICurrentSession cs, CTP_TESTContext context = null) {
            if (context == null) {
                context = ts.CreateContext();
            }
            #region add repositories for CourseService
            CourseRepository cr = new(context);
            UserRepository ur = new(context); 
            TeacherRepository tr = new(context); 
            StudentRepository sr = new(context); 
            ClaraRepository clr = new(context);
            #endregion
            ICourseService course = new CourseService(cr, ur, sr, tr, clr);
            ILanguageService lang = new LanguageService(new LanguageRepository(context));
            MethodSignatureRepository msr = new(context);
            ITestCaseService itcs = new TestCaseService(new TestCaseRepository(context, msr));
            ICourseSettingService courseset = new CourseSettingService(new CourseSettingRepository(context));
            CourseController cc = new(cs, course, courseset, lang, itcs);
            cc.TempData = new Mock<ITempDataDictionary>().Object;
            return cc;
        }
        [Fact]
        public async Task IndexReturnsMainView() {
            // Arrange
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserATeacher()).Returns(true);
            CourseController mockController = new(mockSession.Object, new Mock<ICourseService>().Object, new Mock<ICourseSettingService>().Object, new Mock<ILanguageService>().Object, new Mock<ITestCaseService>().Object);
            // Act
            IActionResult result = await mockController.Index();
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task Details_ReturnsDetailsView() {
            // Arrange
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserATeacher()).Returns(true);
            CTP_TESTContext ctx = ts.CreateContext();

            CourseController mockController = CreateController(mockSession.Object, ctx);
            // Act
            IActionResult result = await mockController.Details(1);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task Details_CourseDontExist() {
            // Arrange
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserATeacher()).Returns(true);
            CTP_TESTContext ctx = ts.CreateContext();

            CourseController mockController = CreateController(mockSession.Object, ctx);
            // Act
            IActionResult result = await mockController.Details(-1);
            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }
        [Fact]
        public async Task StudentDetails_ReturnsDetailsView() {
            // Arrange
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserATeacher()).Returns(true);
            CTP_TESTContext ctx = ts.CreateContext();

            CourseController mockController = CreateController(mockSession.Object, ctx);
            // Act
            IActionResult result = await mockController.StudentDetails(1);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
    }
}
