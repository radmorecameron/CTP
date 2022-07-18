using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Repositories;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services;
using CodeTestingPlatform.Services.Interfaces;
using CTPTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Models.Services {
    public class CodeUploadServiceTest {
        private readonly TestSetup ts;
        public CodeUploadServiceTest() {
            ts = new();
        }
        public ICodeUploadService CreateCodeUploadService() {
            CTP_TESTContext ctx = ts.CreateContext();
            ICodeUploadRepository icur = new CodeUploadRepository(ctx);
            ICodeUploadService cus = new CodeUploadService(icur);
            return cus;
        }
        [Fact]
        public async Task FindByAsync() {
            ICodeUploadService cuService = CreateCodeUploadService();
            CodeUpload a = await cuService.FindByIdAsync(1);
            Assert.NotNull(a);
        }
        [Fact]
        public async Task CreateAsync() {
            ICodeUploadService cuService = CreateCodeUploadService();
            CodeUpload cu = new() {
                ActivityId = 1,
                CodeUploadFileName = "joemama.py",
                CodeUploadText = "wpphpp",
                CodeUploadFile = null,
                CodeUploadId = 3,
                StudentId = 26231,
                UploadDate = DateTime.Now
            };
            await cuService.CreateAsync(cu);
            Assert.True(true);
        }
        [Fact]
        public async Task UpdateAsync() {
            ICodeUploadService cuService = CreateCodeUploadService();
            CodeUpload cu = await cuService.FindByIdAsync(1);
            cu.UploadDate = DateTime.Now;
            await cuService.UpdateAsync(cu);
            Assert.True(true);
        }
        [Fact]
        public async Task DeleteAsync() {
            ICodeUploadService cuService = CreateCodeUploadService();
            CodeUpload cu = await cuService.FindByIdAsync(1);
            await cuService.DeleteAsync(cu);
            Assert.True(true);
        }
        [Fact]
        public async Task FindByStudentIdAndActivityIdAsync() {
            CTP_TESTContext ctx = ts.CreateContext();
            ICodeUploadRepository icur = new CodeUploadRepository(ctx);
            CodeUploadService cuService = new(icur);
            CodeUpload cu = await cuService.FindByStudentIdAndActivityIdAsync(26026, 1);
            Assert.NotNull(cu);
        }
    }
}
