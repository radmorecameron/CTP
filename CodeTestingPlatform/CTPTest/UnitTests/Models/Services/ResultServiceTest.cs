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
    public class ResultServiceTest {
        private readonly TestSetup ts;
        public ResultServiceTest() {
            ts = new();
        }
        public IResultService CreateResultService() {
            CTP_TESTContext ctx = ts.CreateContext();
            IResultRepository irr = new ResultRepository(ctx);
            IResultService irs = new ResultService(irr);
            return irs;
        }
        [Fact]
        public async Task FindByIdAsync() {
            IResultService resService = CreateResultService();
            Result a = await resService.FindByIdAsync(1);
            Assert.NotNull(a);
        }
        [Fact]
        public async Task ListAsync() {
            IResultService resService = CreateResultService();
            IList<Result> a = await resService.ListAsync(1);
            Assert.NotEmpty(a);
        }
        [Fact]
        public async Task PagesListAsync() {
            IResultService resService = CreateResultService();
            IPagedList<Result> a = await resService.ListAsync(0,1);
            Assert.NotEmpty(a.Items);
        }
        [Fact]
        public async Task CreateAsync() {
            IResultService resService = CreateResultService();
            List<Result> rList = new() {
                new() {
                    CodeUploadId = 1,
                    ErrorMessage = "uh oh",
                    PassFail = false,
                    ResultId = 10,
                    TestCaseId = 6
                }
            };
            await resService.CreateAsync(1, rList.ToArray());
            Assert.True(true);
        }
        [Fact]
        public async Task UpdateAsync() {
            IResultService resService = CreateResultService();
            Result res = await resService.FindByIdAsync(1);
            res.ActualValue = "123";
            await resService.UpdateAsync(res);
            Assert.True(true);
        }
        [Fact]
        public async Task DeleteAsync() {
            IResultService resService = CreateResultService();
            await resService.DeleteAsync(1);
            Assert.True(true);
        }
        [Fact]
        public async Task ExistsAsync() {
            IResultService resService = CreateResultService();
            bool exists = await resService.ExistsAsync(1);
            Assert.True(exists);
        }
    }
}
