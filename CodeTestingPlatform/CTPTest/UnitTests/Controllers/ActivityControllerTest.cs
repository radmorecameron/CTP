using CodeTestingPlatform.Controllers;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Repositories;
using CodeTestingPlatform.Services;
using CodeTestingPlatform.Services.Interfaces;
using CTPTest.Helpers;
using Microsoft.AspNetCore.Http;
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
    public class ActivityControllerTest : Controller {
        public readonly TestSetup ts;
        public ActivityControllerTest() {
            ts = new();
        }
        public ActivityController CreateController(CTP_TESTContext context) {
            var httpContext = new DefaultHttpContext();
            IActivityService act = new ActivityService(new ActivityRepository(context));
            CourseRepository cr = new(context); UserRepository ur = new(context); TeacherRepository tr = new(context); StudentRepository sr = new(context); ClaraRepository clr = new(context);
            ICourseService course = new CourseService(cr, ur, sr, tr, clr);
            ILanguageService lang = new LanguageService(new LanguageRepository(context));
            IActivityTypeService iats = new ActivityTypeService(new ActivityTypeRepository(context));
            ICourseSettingService courseset = new CourseSettingService(new CourseSettingRepository(context));
            IStudentService stud = new StudentService(new StudentRepository(context));
            ActivityController ac = new(act, iats, course, courseset, lang, stud);
            ac.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            return ac;
        }
        [Fact]
        public async Task Index_ReturnsMainView() {
            // Arrange
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            // Act
            IActionResult result = await mockController.Index();
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task Details_ReturnsMainView() {
            // Arrange
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            // Act
            IActionResult result = await mockController.Details(1);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task Create_ReturnsMainView() {
            // Arrange
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            mockController.TempData["DefaultLanguage"] = 38;
            // Act
            IActionResult result = await mockController.Create(1, "");
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task Create_AddActivity_Success() {
            // Arrange
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);

            IActionResult result = await mockController.Create(new Activity {
                ActivityTypeId = 1,
                EndDate = DateTime.Now.AddDays(2),
                StartDate = DateTime.Now,
                CourseId = 1,
                LanguageId = 38,
                Title = "MyTitle"
            });
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }
        [Fact]
        public async Task Create_AddActivity_Fail () {
            // Arrange
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            mockController.ModelState.AddModelError("", "");
            IActionResult result = await mockController.Create(new Activity {
                ActivityTypeId = 1,
                Title = "",
                EndDate = DateTime.Now.AddDays(2),
                StartDate = DateTime.Now,
            });
            
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task Edit_ReturnsMainView() {
            // Arrange
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            // Act
            IActionResult result = await mockController.Edit(1, "");
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task Delete_ReturnsMainView() {
            // Arrange
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            // Act
            IActionResult result = await mockController.Delete(1);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task Edit_NonExistant_ActivityId() {
            // Arrange
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            // Act
            IActionResult result = await mockController.Edit(-1, "");
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task Edit_Existing_Activity() {
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            ActivityRepository ar = new(ctx);
            Activity a = await ar.FindOneAsync(a=>a.ActivityId == 1);
            a.Title = "New Title";
            IActionResult result = await mockController.Edit(a);
        }
        [Fact]
        public async Task Edit_Existing_Activity_From_Course() {
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            ActivityRepository ar = new(ctx);
            mockController.TempData["Action"] = "Course";
            Activity a = await ar.FindOneAsync(a => a.ActivityId == 1);
            a.Title = "New Title";
            IActionResult result = await mockController.Edit(a);
        }
        [Fact]
        public async Task Edit_Existing_Activity_With_ModelError() {
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            ActivityRepository ar = new(ctx);
            mockController.ModelState.AddModelError("", "");
            Activity a = await ar.FindOneAsync(a => a.ActivityId == 1);
            a.Title = "New Title";
            IActionResult result = await mockController.Edit(a);
        }
        [Fact]
        public async Task Delete_NonExistant_ActivityId() {
            // Arrange
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            // Act
            IActionResult result = await mockController.Delete(-1);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task DeleteConfirmed() {
            // Arrange
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            ActivityRepository ar = new(ctx);
            // Act
            Activity a = await ar.FindOneAsync(a => a.ActivityId == 1);
            IActionResult result = await mockController.DeleteConfirm(a);
            // Assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }
        [Fact]
        public async Task DeleteNoWarning() {
            // Arrange
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            ActivityRepository ar = new(ctx);
            ActivityService aser = new(ar);
            await aser.CreateAsync(new Activity {
                ActivityTypeId = 1,
                EndDate = DateTime.Now.AddDays(2),
                StartDate = DateTime.Now,
                CourseId = 1,
                LanguageId = 38,
                Title = "MyTitle",
                ActivityId = 998
            });
            var result = await mockController.Delete(998);
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task Details_Exists() {
            // Arrange
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            ActivityRepository ar = new(ctx);
            // Act
            IActionResult result = await mockController.Details(1);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task Details_NotExists() {
            // Arrange
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            ActivityRepository ar = new(ctx);
            // Act
            IActionResult result = await mockController.Details(-1);
            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }
        [Fact]
        public async Task InvalidTestCase_NullRequired() {
            // Arrange
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            ActivityRepository ar = new(ctx);
            ActivityService aser = new(ar);
            Activity a = await aser.FindByIdAsync(1);
            MethodSignature ms = a.MethodSignatures.FirstOrDefault();
            TestCase tc = ms.TestCases.FirstOrDefault();
            Parameter p = tc.Parameters.FirstOrDefault();
            p.SignatureParameter.RequiredParameter = true;
            // p.Value = null;
            ParameterRepository pr = new(ctx);
            ParameterService ps = new(pr);
            await ps.UpdateAsync(p);
            // Act
            IActionResult result = await mockController.Details(1);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task InvalidTestCase_WrongDataType() {
            // Arrange
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            ActivityRepository ar = new(ctx);
            Activity a = await ar.FindOneAsync(a => a.ActivityId == 1);
            MethodSignature ms = a.MethodSignatures.FirstOrDefault();
            TestCase tc = ms.TestCases.FirstOrDefault();
            Parameter p = tc.Parameters.FirstOrDefault();
            p.Value = "Hello World";
            ParameterRepository pr = new(ctx);
            await pr.UpdateAsync(p);
            // Act
            IActionResult result = await mockController.Details(1);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task ActivityResultsView() {
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            // Act
            IActionResult result = await mockController.ActivityResults(1);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task ActivityResultsView_ActivityNotFound() {
            using var ctx = ts.CreateContext();
            ActivityController mockController = CreateController(ctx);
            // Act
            IActionResult result = await mockController.ActivityResults(-1);
            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }
    }
}
