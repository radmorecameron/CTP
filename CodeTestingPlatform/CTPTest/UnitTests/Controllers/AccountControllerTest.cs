using CodeTestingPlatform.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Controllers {
    public class AccountControllerTest {
        [Fact]
        public void Index_ReturnsMainView() {
            // Arrange
            AccountController mockController = new();
            // Act
            IActionResult result = mockController.Index();
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public void AccessDenied_ReturnsMainView() {
            // Arrange
            AccountController mockController = new();
            // Act
            IActionResult result = mockController.AccessDenied();
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
    }
}
