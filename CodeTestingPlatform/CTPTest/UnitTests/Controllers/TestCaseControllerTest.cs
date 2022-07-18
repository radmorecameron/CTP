using CodeTestingPlatform.Controllers;
using CodeTestingPlatform.DatabaseEntities.Local;
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
    public class TestCaseControllerTest {
        public readonly TestSetup ts;
        public TestCaseControllerTest() {
            ts = new();
        }
        public TestCaseController CreateController(CTP_TESTContext ctx = null) {
            if (ctx == null) {
                ctx = ts.CreateContext();
            }
            MethodSignatureRepository msr = new(ctx);
            ITestCaseService itcs = new TestCaseService(new TestCaseRepository(ctx, msr));
            IMethodSignatureService ims = new MethodSignatureService(new MethodSignatureRepository(ctx));
            IDataTypeService ids = new DataTypeService(new DataTypeRepository(ctx));
            IParameterService ips = new ParameterService(new ParameterRepository(ctx));
            IExceptionService ies = new ExceptionService(new ExceptionRepository(ctx));
            TestCaseController mockController = new(itcs, ims, ids, ips, ies);
            mockController.TempData = new Mock<ITempDataDictionary>().Object;

            return mockController;
        }
        [Fact]
        public async Task IndexReturnsMainView() {
            // Arrange
            TestCaseController mockController = CreateController();
            // Act
            IActionResult result = await mockController.Index();
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public async Task Delete_NonExistant_TestCase() {
            // Arrange & Act
            TestCaseController controller = CreateController();
            // Act
            IActionResult result = await controller.Delete(-5);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task Edit_NonExistant_TestCase() {
            // Arrange & Act
            TestCaseController controller = CreateController();
            // Act
            IActionResult result = await controller.Edit(-5);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task Edit_NonExistant_TestCase2() {
            // Arrange & Act
            CTP_TESTContext ctx = ts.CreateContext();
            TestCaseController controller = CreateController(ctx);
            MethodSignatureRepository msr = new(ctx);
            TestCaseRepository tcr = new(ctx, msr);
            TestCase tc = await tcr.FindByIdAsync(1);
            tc.TestCaseId = -5;
            // Act
            IActionResult result = await controller.Edit(-5, null, null, 0);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task Edit_Invalid_TestCase() {
            // Arrange & Act
            TestCaseController controller = CreateController();
            controller.ModelState.AddModelError("error", "some error");
            // Act
            IActionResult result = await controller.Edit(-5);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task EditReturnsMainView() {
            // Arrange & Act
            TestCaseController controller = CreateController();
            // Act
            IActionResult result = await controller.Edit(1);
            // Assert
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async Task Edit_Valid_TestCase() {
            // Arrange & Act
            CTP_TESTContext ctx = ts.CreateContext();
            TestCaseController controller = CreateController(ctx);
            MethodSignatureRepository msr = new(ctx);
            TestCaseRepository tcr = new(ctx, msr);
            TestCase tc = await tcr.FindByIdAsync(1);
            tc.TestCaseName = "newName";
            // Act
            IActionResult result = await controller.Edit(tc.TestCaseId, tc, tc.Parameters.ToArray(), -1);
            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task CreateTestCase_ReturnsMainView() {
            // Arrange & Act
            CTP_TESTContext ctx = ts.CreateContext();
            TestCaseController controller = CreateController(ctx);
            IActionResult result = await controller.Create(1,"activity");
            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Valid_TestCase() {
            // Arrange & Act
            CTP_TESTContext ctx = ts.CreateContext();
            TestCaseController controller = CreateController(ctx);
            MethodSignatureRepository msr = new(ctx);
            TestCaseRepository tcr = new(ctx, msr);
            TestCase tc = (await tcr.FindByIdAsync(1));
            tc.TestCaseName = "coolAwesomeTestCase";
            tc.TestCaseId = 999;
            tc.MethodSignature = null;
            // Act
            IActionResult result = await controller.Create(tc, tc.Parameters.ToArray(),-1);
            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }
        [Fact]
        public async Task Create_TestCase_NameAlreadyExists() {
            // Arrange & Act
            CTP_TESTContext ctx = ts.CreateContext();
            TestCaseController controller = CreateController(ctx);
            MethodSignatureRepository msr = new(ctx);
            TestCaseRepository tcr = new(ctx, msr);
            TestCase tc = (await tcr.FindByIdAsync(1));
            tc.TestCaseId = 999;
            tc.MethodSignature = null;
            // Act
            IActionResult result = await controller.Create(tc, tc.Parameters.ToArray(),-1);
            // Assert
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async Task Details_ReturnsMainView() {
            // Arrange & Act
            TestCaseController controller = CreateController();
            // Act
            IActionResult result = await controller.Details(1);
            // Assert
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async Task Details_TestCaseDoesntExist() {
            // Arrange & Act
            TestCaseController controller = CreateController();
            // Act
            IActionResult result = await controller.Details(-1);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task DeleteConfirmed_TestCaseExists() {
            // Arrange & Act
            TestCaseController controller = CreateController();
            // Act
            IActionResult result = await controller.DeleteConfirmed(1);
            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }
        [Fact]
        public async Task DeleteConfirmed_TestCaseNotExists() {
            // Arrange & Act
            TestCaseController controller = CreateController();
            // Act
            IActionResult result = await controller.DeleteConfirmed(-1);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
