using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using Xunit;

namespace CTPTest.UnitTests.Controllers {
    public class SignatureControllerTest {
        public readonly TestSetup ts;
        public SignatureControllerTest() {
            ts = new();
        }
        public MethodSignatureController GetMockController(CTP_TESTContext ctx = null) {
            if (ctx == null) {
                ctx = ts.CreateContext();
            }
            IMethodSignatureService ims = new MethodSignatureService(new MethodSignatureRepository(ctx));
            IActivityService ias = new ActivityService(new ActivityRepository(ctx));
            IDataTypeService ids = new DataTypeService(new DataTypeRepository(ctx));
            IParameterService ips = new ParameterService(new ParameterRepository(ctx));
            MethodSignatureRepository msr = new(ctx);
            ITestCaseService itcs = new TestCaseService(new TestCaseRepository(ctx, msr));
            IExceptionService es = new ExceptionService(new ExceptionRepository(ctx));
            IUserDefinedExceptionService iues = new UserDefinedExceptionService(new UserDefinedExceptionRepository(ctx));
            MethodSignatureController msc = new(ims, ias, ids, ips, itcs, es, iues);
            
            msc.TempData = new Mock<ITempDataDictionary>().Object;
            return msc;
        }

        [Fact]
        public void Create_ReturnsMainView() {
            // Arrange
            MethodSignatureController mockController = GetMockController();
            // Act
            IActionResult result = mockController.Create(1).Result;
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task Edit_ReturnsMainView() {
            // Arrange
            MethodSignatureController mockController = GetMockController();
            // Act
            IActionResult result = await mockController.Edit(1);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task Details_ReturnsMainView() {
            // Arrange
            MethodSignatureController mockController = GetMockController();
            // Act
            IActionResult result = await mockController.Details(1);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task Create_ReturnsBadRequest_GivenInvalidModel() {
            // Arrange & Act
            CTP_TESTContext ctx = ts.CreateContext();
            MethodSignatureController mockController = GetMockController(ctx);
            mockController.ModelState.AddModelError("error", "some error");
            MethodSignatureRepository msr = new(ctx);
            // Act
            MethodSignature ms = await msr.FindByIdAsync(1);
            ms.SignatureId = 9999;      
            IActionResult result = await mockController.Create(model: ms, parameters: ms.SignatureParameters.ToArray(), from: null, exceptionIdsString: null, null);
            // Assert
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async Task Create_Success_OnValidModel() {
            // Arrange & Act
            CTP_TESTContext ctx = ts.CreateContext();
            MethodSignatureController mockController = GetMockController(ctx);
            MethodSignatureRepository msr = new(ctx);
            // Act
            MethodSignature ms = await msr.FindByIdAsync(1);
            ms.SignatureId = 9999;
            IActionResult result = await mockController.Create(model: ms, parameters: ms.SignatureParameters.ToArray(), from: null, exceptionIdsString: null, null);
            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }
        [Fact]
        public void Create_Returns_NotFound_On_InvalidActivity() {
            // Arrange
            MethodSignatureController mockController = GetMockController();
            // Act
            IActionResult result = mockController.Create(null).Result;
            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }
        [Fact]
        public async Task Edit_ReturnsBadRequest_GivenInvalidModel() {
            // Arrange & Act
            MethodSignatureController mockController = GetMockController();
            mockController.ModelState.AddModelError("error", "some error");
            // Act
            IActionResult result = await mockController.Edit(id: 1, model: null, parameters: null, null, null);
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task Edit_Returns_NotFound_On_InvalidActivity() {
            // Arrange
            MethodSignatureController mockController = GetMockController();
            // Act
            IActionResult result = await mockController.Edit(-1);
            // Assert
            Assert.IsAssignableFrom<NotFoundObjectResult>(result);
        }
        [Fact]
        public async Task Edit_Valid_Signature() {
            CTP_TESTContext ctx = ts.CreateContext();
            MethodSignatureController mockController = GetMockController(ctx);
            MethodSignatureRepository msr = new(ctx);
            MethodSignature ms = await msr.FindByIdAsync(1);
            ms.MethodName = "test_val";
            // ACT
            IActionResult result = await mockController.Edit(ms.SignatureId, ms, ms.SignatureParameters.ToArray(), null, null);
            // ASSERT
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }
        [Fact]
        public async Task Edit_Invalid_Signature() {
            CTP_TESTContext ctx = ts.CreateContext();
            MethodSignatureController mockController = GetMockController(ctx);
            mockController.ModelState.AddModelError("error", "msg");
            MethodSignatureRepository msr = new(ctx);
            MethodSignature ms = await msr.FindByIdAsync(1);
            ms.MethodName = "test_val";
            // ACT
            IActionResult result = await mockController.Edit(ms.SignatureId, ms, ms.SignatureParameters.ToArray(), null, null);
            // ASSERT
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task Edit_Invalid_Signature_Name() {
            CTP_TESTContext ctx = ts.CreateContext();
            MethodSignatureController mockController = GetMockController(ctx);
            mockController.ModelState.AddModelError("error", "msg");
            MethodSignatureRepository msr = new(ctx);
            MethodSignature ms = await msr.FindByIdAsync(1);
            ms.MethodName = "TEST";
            // ACT
            IActionResult result = await mockController.Edit(ms.SignatureId, ms, ms.SignatureParameters.ToArray(), null, null);
            // ASSERT
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public async Task Delete_NonExistant_Signature() {
            // Arrange & Act
            MethodSignatureController mockController = GetMockController();
            // Act
            IActionResult result = await mockController.Delete(-5);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task Delete_Confirmed_NonExistant_Signature() {
            // Arrange & Act
            MethodSignatureController mockController = GetMockController();
            // Act
            IActionResult result = await mockController.DeleteConfirmed(-5);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task Delete_Confirmed_Existant_Signature() {
            // Arrange & Act
            MethodSignatureController mockController = GetMockController();
            // Act
            IActionResult result = await mockController.DeleteConfirmed(1);
            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}