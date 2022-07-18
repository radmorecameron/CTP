using CodeTestingPlatform.Controllers;
using CTPTest.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Controllers {
    public class HomeControllerTest {
        [Fact]
        public void IndexReturnsMainView() {
            // Arrange
            Mock<ILogger<HomeController>> mock = new();
            ILogger<HomeController> logger = mock.Object;
            HomeController mockController = new (logger);
            // Act
            IActionResult result = mockController.Index();
            // Assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }
        [Fact]
        public void Privacy_ReturnsMainView() {
            // Arrange
            Mock<ILogger<HomeController>> mock = new();
            ILogger<HomeController> logger = mock.Object;
            HomeController mockController = new(logger);
            // Act
            IActionResult result = mockController.Privacy();
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public void Help_ReturnsMainView() {
            // Arrange
            Mock<ILogger<HomeController>> mock = new();
            ILogger<HomeController> logger = mock.Object;
            HomeController mockController = new(logger);
            // Act
            IActionResult result = mockController.Help();
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public void About_ReturnsMainView() {
            // Arrange
            Mock<ILogger<HomeController>> mock = new();
            ILogger<HomeController> logger = mock.Object;
            HomeController mockController = new(logger);
            // Act
            IActionResult result = mockController.About();
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public void ReleaseNotes_ReturnsMainView() {
            // Arrange
            Mock<ILogger<HomeController>> mock = new();
            ILogger<HomeController> logger = mock.Object;
            HomeController mockController = new(logger);
            // Act
            IActionResult result = mockController.ReleaseNotes();
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public void StatusReturnsErrorPage() {
            // Arrange
            Mock<ILogger<HomeController>> mock = new();
            ILogger<HomeController> logger = mock.Object;
            HomeController mockController = new(logger);
            // Act
            IActionResult result = mockController.Status(404);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public void NullStatusReturnsErrorPage() {
            // Arrange
            Mock<ILogger<HomeController>> mock = new();
            ILogger<HomeController> logger = mock.Object;
            HomeController mockController = new(logger);
            // Act
            IActionResult result = mockController.Status(-2);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public void SelectLanguage() {
            Mock<ILogger<HomeController>> mock = new();
            ILogger<HomeController> logger = mock.Object;
            HomeController mockController = new(logger);
            HeaderDictionary headerDictionary = new();
            Mock<IResponseCookies> rc = new();
            Mock<HttpResponse> response = new();
            response.SetupGet(r => r.Headers).Returns(headerDictionary);
            response.Setup(r => r.Cookies).Returns(rc.Object);
            Mock<HttpContext> httpContext = new();

            httpContext.SetupGet(a => a.Response).Returns(response.Object);
            var x = httpContext.Object.Response.Cookies;
            mockController.ControllerContext = new ControllerContext() {
                HttpContext = httpContext.Object
            };
            // Act
            IActionResult result = mockController.SelectLanguage("en-CA", "https://google.com");
            //ASSERT
            Assert.IsAssignableFrom<LocalRedirectResult>(result);
        }
        [Fact]
        public void Error() {
            Mock<ILogger<HomeController>> mock = new();
            ILogger<HomeController> logger = mock.Object;
            HomeController mockController = new(logger);
            HeaderDictionary headerDictionary = new();
            Mock<IResponseCookies> rc = new();
            Mock<HttpResponse> response = new();
            response.SetupGet(r => r.Headers).Returns(headerDictionary);
            response.Setup(r => r.Cookies).Returns(rc.Object);
            Mock<HttpContext> httpContext = new();
            httpContext.SetupGet(a => a.Response).Returns(response.Object);
            var x = httpContext.Object.Response.Cookies;
            mockController.ControllerContext = new ControllerContext() {
                HttpContext = httpContext.Object
            };
            // Act
            IActionResult result = mockController.Error();
            //ASSERT
            Assert.IsAssignableFrom<ViewResult>(result);
        }
    }
}