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
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Controllers {
    public class LoginControllerTest {
        private readonly HtmlEncoder _htmlEncoder = null;
        public readonly TestSetup ts;
        public LoginControllerTest() {
            ts = new();
        }
        private Mock<ICurrentSession> CreateSession(bool isAuthorized = false, bool isStudent = false, bool isTeacher = false, bool isCompSci = false) {
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(isAuthorized);
            mockSession.Setup(session => session.IsUserATeacher()).Returns(isTeacher);
            mockSession.Setup(session => session.IsUserAStudent()).Returns(isStudent);
            mockSession.Setup(session => session.IsCompSciStudent()).Returns(isCompSci);
            return mockSession;
        }
        private LoginController CreateController(ICurrentSession session, CTP_TESTContext ctx = null, HtmlEncoder htmlEncoder = null) {
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
            ITeacherService its = new TeacherService(itr);
            IUserService ius = new UserService(iur, iss, its);
            #endregion
            LoginController lc = new(session, htmlEncoder,cs,ius,its,iss);
            lc.TempData = new Mock<ITempDataDictionary>().Object;
            return lc;
        }
        [Fact]
        public void Index_ReturnsIndex_SessionIsAuthorizedFalse() {
            Mock<ICurrentSession> mockSession = CreateSession();
            LoginController controller = CreateController(mockSession.Object);
            ViewResult result = controller.Index() as ViewResult;
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        [Fact]
        public void IndexGet_ReturnsStudentIndex_SessionIsAuthorizedFalse_UserTypeIsStudentNotInCompSci() {
            Mock<ICurrentSession> mockSession = CreateSession(isAuthorized: true, isStudent: true);
            LoginController controller = CreateController(mockSession.Object);
            RedirectToActionResult result = (RedirectToActionResult)controller.Index();
            string expected = "Student/Index";
            string resultUrl = $"{result.ControllerName}/{result.ActionName}";
            Assert.Equal(expected, resultUrl);
        }

        [Fact]
        public void IndexGet_ReturnsStudentIndex_SessionIsAuthorizedTrue_UserTypeIsStudent() {
            Mock<ICurrentSession> mockSession = CreateSession(isAuthorized: true, isStudent: true, isCompSci: true);
            LoginController controller = CreateController(mockSession.Object);
            RedirectToActionResult result = (RedirectToActionResult)controller.Index();
            string expected = "Student/Index";
            string resultUrl = $"{result.ControllerName}/{result.ActionName}";
            Assert.Equal(expected, resultUrl);
        }

        [Fact]
        public void IndexGet_ReturnsStudentIndex_SessionIsAuthorizedTrue_UserTypeIsTeacher() {
            Mock<ICurrentSession> mockSession = CreateSession(isAuthorized: true, isTeacher: true);
            LoginController controller = CreateController(mockSession.Object);
            RedirectToActionResult result = (RedirectToActionResult)controller.Index();
            string expected = "Teacher/Index";
            string resultUrl = $"{result.ControllerName}/{result.ActionName}";
            Assert.Equal(expected, resultUrl);
        }

        [Fact]
        public async Task IndexPost_ReturnIndex_ModelStateIsInvalid() {
            Mock<ICurrentSession> mockSession = CreateSession();
            LoginController controller = CreateController(mockSession.Object);
            controller.ModelState.AddModelError("", "");
            Login login = new() {
                Username = "yeehhh"
            };
            string expected = "Index";
            IActionResult result = await controller.Index(login);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == expected);
        }

        [Fact]
        public async Task IndexPost_ReturnIndex_ModelStateIsValid_IsAuthorizedFalse() {
            Mock<ICurrentSession> mockSession = CreateSession();
            LoginController controller = CreateController(mockSession.Object);
            Login login = new() {
                Username = "yeehhh"
            };
            string expected = "Index";
            IActionResult result = await controller.Index(login);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == expected);
            Assert.True(controller.ModelState.IsValid == false);
        }
        public async Task<string> IndexPost_ReturnIndex_ModelStateIsValid_IsAuthorizedTrue(bool isTeacher = false, bool isStudent = false) {
            Mock<ICurrentSession> mockSession= CreateSession(isAuthorized: true, isTeacher:isTeacher, isStudent:isStudent);
            LoginController controller = CreateController(mockSession.Object);
            Login login = new() {
                Username = "yeehhh"
            };
            IActionResult result = await controller.Index(login);
            RedirectToActionResult viewResult = Assert.IsType<RedirectToActionResult>(result);
            string resultUrl = $"{viewResult.ControllerName}/{viewResult.ActionName}";
            return resultUrl;
        }
        [Fact]
        public async Task IndexPost_ReturnIndex_ModelStateIsValid_IsAuthorizedTrue_Student() {
            string result = await IndexPost_ReturnIndex_ModelStateIsValid_IsAuthorizedTrue(isStudent: true);
            string expected = "Student/Index";
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task IndexPost_ReturnIndex_ModelStateIsValid_IsAuthorizedTrue_Teacher() {
            string result = await IndexPost_ReturnIndex_ModelStateIsValid_IsAuthorizedTrue(isTeacher: true);
            string expected = "Teacher/Index";
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task IndexPost_ReturnError_ModelStateIsValid_IsAuthorizedTrue_Notype() {
            Mock<ICurrentSession> mockSession = CreateSession(isAuthorized: true, isTeacher: false);
            LoginController controller = CreateController(mockSession.Object);
            Login login = new() {
                Username = "yeehhh"
            };
            IActionResult result = await controller.Index(login);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            Assert.True(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Index");
            Assert.True(controller.ModelState.IsValid == false);
        }

       [Fact]
        public void AccessDenied_ReturnsAccessDenied() {
            Mock<ICurrentSession> mockSession = CreateSession(isAuthorized: false);
            LoginController controller = CreateController(mockSession.Object);
            ViewResult result = controller.AccessDenied() as ViewResult;
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "AccessDenied");
        }

        [Fact]
        public void Logout_ReturnIndex() {
            Mock<ICurrentSession> mockSession = CreateSession(isAuthorized: false);
            LoginController controller = CreateController(mockSession.Object);
            RedirectToActionResult result = controller.Logout() as RedirectToActionResult;
            Assert.True(result.ActionName == "Index");
        }
    }
}
