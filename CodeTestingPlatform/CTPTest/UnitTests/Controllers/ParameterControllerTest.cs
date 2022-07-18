using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodeTestingPlatform.Controllers;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories;
using CodeTestingPlatform.Services;
using CodeTestingPlatform.Services.Interfaces;
using CTPTest.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CTPTest.UnitTests.Controllers {
    public class ParameterControllerTest {
        public readonly TestSetup ts;
        public ParameterControllerTest() {
            ts = new();
        }
        public ParameterController CreateController(CTP_TESTContext context = null) {
            if (context == null) {
                context = ts.CreateContext();
            }
            IParameterService ips = new ParameterService(new ParameterRepository(context));
            ParameterController pc = new(ips);
            pc.TempData = new Mock<ITempDataDictionary>().Object;
            return pc;
        }

        [Fact]
        public void IndexReturnsMainView() {
            // Arrange
            ParameterController mockController = CreateController();
            // Act
            IActionResult result = mockController.Index();
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsMainView() {
            // Arrange
            ParameterController mockController = CreateController();
            // Act
            IActionResult result = await mockController.Edit(1);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task Edit_ReturnsNotFound() {
            // Arrange
            ParameterController mockController = CreateController();
            // Act
            IActionResult result = await mockController.Edit(-1);
            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }
        [Fact]
        public async Task Edit_NonExistantParameter_ReturnsError() {
            CTP_TESTContext context = ts.CreateContext();
            // Arrange
            ParameterController mockController = CreateController(context);
            IParameterService ips = new ParameterService(new ParameterRepository(context));
            Parameter param = await ips.FindByIdAsync(1);
            // Act
            IActionResult result = await mockController.Edit(-1, new ParameterDTO(param));
            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }
        [Fact]
        public async Task Edit_ReturnsBadRequest_GivenInvalidModel() {
            CTP_TESTContext context = ts.CreateContext();
            // Arrange
            ParameterController mockController = CreateController(context);
            IParameterService ips = new ParameterService(new ParameterRepository(context));
            Parameter param = await ips.FindByIdAsync(1);
            mockController.ModelState.AddModelError("error", "some error");
            // Act
            IActionResult result = await mockController.Edit(id: 1, parameter: new ParameterDTO(param));
            // Assert
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async Task Edit_DTO_Valid() {
            CTP_TESTContext context = ts.CreateContext();
            // Arrange
            ParameterController mockController = CreateController(context);
            IParameterService ips = new ParameterService(new ParameterRepository(context));
            Parameter param = await ips.FindByIdAsync(1);
            param.Value = "1";
            // Act
            IActionResult result = await mockController.Edit(id: 1, parameter: new ParameterDTO(param));
            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}