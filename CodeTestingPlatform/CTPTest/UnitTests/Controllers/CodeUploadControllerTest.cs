using CodeTestingPlatform.CompilerClient;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Controllers {
    public class CodeUploadControllerTest {
        public readonly TestSetup ts;
        public CodeUploadControllerTest() {
            ts = new();
        }
        public string GetEvensCode() {
            string getEvens = @"def get_even_list(num_list):
    even_list = []
    for el in num_list:
        if el % 2 == 0:
            even_list.append(el)
    return even_list


def get_even_count(num_list):
    return len(get_even_list(num_list))
";
            return getEvens;
        }
        public CodeUploadController CreateController(ICurrentSession cs, CTP_TESTContext context = null) {
            if (context == null) {
                context = ts.CreateContext();
            }
            Compiler comp = new();
            CodeUploadRepository cur = new(context);
            CodeUpload cud = new(cur);
            ActivityRepository ar = new(context);
            ActivityService aserv = new(ar);
            ResultService rs = new(new ResultRepository(context));
            CodeUploadService cus = new(new CodeUploadRepository(context));
            MethodSignatureRepository msr = new(context);
            TestCaseRepository tcr = new(context, msr);
            MethodSignatureService mss = new(msr);
            CodeUploadController cuc = new(cs, comp, cud, aserv, tcr, mss, rs, cus);
            cuc.TempData = new Mock<ITempDataDictionary>().Object;
            return cuc;
        }
        private SourceFile CreateSourceFile(int activityId, string fileName, string content) {
            //Arrange
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            SourceFile sf = new() {
                ActivityId = activityId,
                FileUpload = fileMock.Object
            };
            return sf;
        }
        [Fact]
        public async Task GetCodeSubmission() {
            // Arrange
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserATeacher()).Returns(true);
            CodeUploadController mockController = CreateController(mockSession.Object);
            // Act
            IActionResult result = await mockController.CodeSubmission(1);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task GetCodeSubmission_Exists() {
            // Arrange
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserATeacher()).Returns(true);
            mockSession.Setup(session => session.IsUserAStudent()).Returns(true);
            mockSession.Setup(session => session.GetStudentId()).Returns(26026);
            CodeUploadController mockController = CreateController(mockSession.Object);
            string content = GetEvensCode();

            SourceFile sf = CreateSourceFile(1, "test.py", content);
            var _sourceFileResult = await mockController.SourceFile(sf);
            // Act
            IActionResult result = await mockController.CodeSubmission(1);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public void SourceFile_ReturnsView() {
            // Arrange
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserATeacher()).Returns(true);
            CodeUploadController mockController = CreateController(mockSession.Object);
            // Act
            IActionResult result = mockController.SourceFile();
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task SourceFile_Valid() {
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserATeacher()).Returns(true);
            mockSession.Setup(session => session.IsUserAStudent()).Returns(true);
            mockSession.Setup(session => session.GetStudentId()).Returns(26026);
            CodeUploadController mockController = CreateController(mockSession.Object);
            string content = GetEvensCode();
            SourceFile sf = CreateSourceFile(1, "test.py", content);
            // Act
            IActionResult result = await mockController.SourceFile(sf);
            // Assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }
        [Fact]
        public async Task GetCodeSubmission_InvalidActivityId() {
            // Arrange
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserATeacher()).Returns(true);
            CodeUploadController mockController = CreateController(mockSession.Object);
            // Act
            IActionResult result = await mockController.CodeSubmission(-1);
            // Assert
            Assert.IsAssignableFrom<NotFoundObjectResult>(result);
        }
        [Fact]
        public async Task SourceFile_Invalid() {
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserATeacher()).Returns(true);
            CodeUploadController mockController = CreateController(mockSession.Object);
            SourceFile sf = new() {
                ActivityId = 1
            };
            mockController.ModelState.AddModelError("", "");
            // Act
            IActionResult result = await mockController.SourceFile(sf);
            // Assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }
        [Fact]
        public async Task SourceFile_InvalidActivityId() {
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserATeacher()).Returns(true);
            CodeUploadController mockController = CreateController(mockSession.Object);
            SourceFile sf = new() {
                ActivityId = -1
            };
            // Act
            IActionResult result = await mockController.SourceFile(sf);
            // Assert
            Assert.IsAssignableFrom<NotFoundObjectResult>(result);
        }
        [Fact]
        public void SourceCode_Get() {
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserATeacher()).Returns(true);
            CodeUploadController mockController = CreateController(mockSession.Object);
            // Act
            IActionResult result = mockController.SourceCode();
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public void SourceCode_Post() {
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserATeacher()).Returns(true);
            CodeUploadController mockController = CreateController(mockSession.Object);
            SourceCode sc = new() {
                CodeText = "print('hello')"
            };
            // Act
            IActionResult result = mockController.SourceCode(sc);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public void SourceCode_PostInvalid() {
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserATeacher()).Returns(true);
            CodeUploadController mockController = CreateController(mockSession.Object);
            mockController.ModelState.AddModelError("", "");
            SourceCode sc = new() {
                CodeText = "print('hello')"
            };
            // Act
            IActionResult result = mockController.SourceCode(sc);
            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task CodeSubmission_NoTestCases () {
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserAStudent()).Returns(true);
            mockSession.Setup(session => session.GetStudentId()).Returns(26026);
            CodeUploadController mockController = CreateController(mockSession.Object);
            string content = GetEvensCode();

            SourceFile sf = CreateSourceFile(1, "test.py", content);
            var _sourceFile = await mockController.SourceFile(sf);
            // Act
            IActionResult result = await mockController.CodeSubmission(1, null);
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task CodeSubmission_NullActivity() {
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserAStudent()).Returns(true);
            mockSession.Setup(session => session.GetStudentId()).Returns(26026);
            CodeUploadController mockController = CreateController(mockSession.Object);
            IActionResult result = await mockController.CodeSubmission(-1, "");
            Assert.IsAssignableFrom<NotFoundObjectResult>(result);
        }
        [Fact]
        public async Task CodeSubmission_TestCases() {
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserAStudent()).Returns(true);
            mockSession.Setup(session => session.GetStudentId()).Returns(26026);
            CodeUploadController mockController = CreateController(mockSession.Object);
            string content = GetEvensCode();

            SourceFile sf = CreateSourceFile(1, "test.py", content);
            var _sourceFile = await mockController.SourceFile(sf);
            // Act
            IActionResult result = await mockController.CodeSubmission(1, "1,2");
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task CodeSubmission_CompilerError() {
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserAStudent()).Returns(true);
            mockSession.Setup(session => session.GetStudentId()).Returns(26026);
            CodeUploadController mockController = CreateController(mockSession.Object);
            string content = GetEvensCode();
            SourceFile sf = CreateSourceFile(1, "test.py", "invalid python");
            var _sourceFile = await mockController.SourceFile(sf);
            // Act
            IActionResult result = await mockController.CodeSubmission(1, "1,2");
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [Fact]
        public async Task CodeSubmission_SourceFileDoesntExist() {
            Mock<ICurrentSession> mockSession = new();
            mockSession.Setup(session => session.IsAuthorized()).Returns(true);
            mockSession.Setup(session => session.IsUserAStudent()).Returns(true);
            mockSession.Setup(session => session.GetStudentId()).Returns(24472);
            CodeUploadController mockController = CreateController(mockSession.Object);
            // Act
            IActionResult result = await mockController.CodeSubmission(1, "1,2");
            Assert.IsAssignableFrom<ViewResult>(result);
        }
    }
}
