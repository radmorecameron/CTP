using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Repositories;
using CTPTest.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Models.repository {
    [ExcludeFromCodeCoverage]
    public class ResultRepoTest {
        public readonly TestSetup ts;
        public ResultRepoTest() {
            ts = new();
        }

        [Fact]
        public async Task Result_ListQuery() {
            using var context = ts.CreateContext();
            ResultRepository _repository = new(context);

            var results = await _repository.GetAllAsync(
                predicate: a => a.CodeUploadId == 1,
                include: q => q.Include(r => r.TestCase),
                orderBy: q => q.OrderBy(a => a.ResultId));


            Assert.Null(results[0].ErrorMessage);

            var result = results.ElementAt(0);

            Assert.NotNull(result.TestCase);
        }

        [Fact]
        public async Task Result_ListAsyncWithPageable() {
            using var context = ts.CreateContext();
            ResultRepository _repository = new(context);

            var results = await _repository.GetAllAsync(
                predicate: a => a.CodeUploadId == 1, // Filter 
                orderBy: q => q.OrderBy(r => r.ResultId), // Sort by ResultId
                include: q => q.Include(a => a.TestCase)); // Include TestCase 

            Assert.Null(results[0].ErrorMessage);

            var result = results[0];

            Assert.NotNull(result.TestCase);
        }

        [Fact]
        public async Task Result_ListAsyncWithoutPageable() {
            using var context = ts.CreateContext();
            ResultRepository _repository = new(context);

            var results = await _repository.GetAllAsync(
                a => a.CodeUploadId == 1, // Filter 
                q => q.OrderBy(a => a.ResultId), // Sort by ResultId
                q => q.Include(a => a.TestCase)); // Include TestCase, 

            Assert.Null(results[0].ErrorMessage);

            var result = results.ElementAt(0);

            Assert.NotNull(result.TestCase);
        }

        [Fact]
        public async Task Result_ListAsyncWithCodeUploadId() {
            using var context = ts.CreateContext();
            ResultRepository _repository = new(context);
            IEnumerable<Result> _results = await _repository.GetAllAsync(predicate: a => a.CodeUploadId == 1);
            Assert.Null(_results.ElementAt(0).ErrorMessage);
        }

        [Fact]
        public async Task Result_ListAsyncWithoutResultId() {
            using var context = ts.CreateContext();
            ResultRepository _repository = new(context);
            IEnumerable<Result> _results = await _repository.GetAllAsync();
            Assert.Null(_results.ElementAt(0).ErrorMessage);
        }

        [Fact]
        public async Task Result_FindByIdValid() {
            using var context = ts.CreateContext();
            ResultRepository _repository = new(context);
            Result act = await _repository.FindOneAsync(t => t.ResultId == 1);
            Assert.Null(act.ErrorMessage);
        }

        [Fact]
        public async Task Result_FindByIdInvalid() {
            using var context = ts.CreateContext();
            ResultRepository _repository = new(context);
            Result act = await _repository.FindOneAsync(t => t.ResultId == -1);
            Assert.Null(act);
        }

        [Fact]
        public async Task Result_CreateAsyncValid() {
            using var context = ts.CreateContext();

            ResultRepository _repository = new(context);
            Result result = new() {
                ResultId = 6,
                PassFail = true,
                CodeUploadId = 1,
                ActualValue = "0",
                ErrorMessage = "Error Message",
                TestCaseId = 1
            };

            _repository.Add(result);
            await _repository.SaveChangesAsync();
            Assert.True(true);
        }

        [Fact]
        public async Task Result_CreateAsyncInvalid() {
            using var context = ts.CreateContext();

            ResultRepository _repository = new(context);
            Result result = new() {
                ResultId = 6,
                PassFail = true,
                CodeUploadId = 1,
                ActualValue = "0",
                ErrorMessage = "Error Message"
            };
            _repository.Add(result);
            await _repository.SaveChangesAsync();
            Assert.True(false);
        }

        [Fact]
        public async Task Result_Update_Change_ResultId() {
            using var context = ts.CreateContext();
            ResultRepository _repository = new(context);
            Result act = await _repository.FindOneAsync(t => t.ResultId == 1);
            _repository.Update(act);
            await _repository.SaveChangesAsync();
            Assert.True(true);
        }

        [Fact]
        public async Task Result_DeleteAsync() {
            using var context = ts.CreateContext();

            ResultRepository _repository = new(context);
            Result result = new() {
                ResultId = 6,
                PassFail = true,
                CodeUploadId = 1,
                ActualValue = "0",
                ErrorMessage = "Error Message"
            };
            _repository.Add(result);
            await _repository.DeleteAsync(result.ResultId);
            await _repository.SaveChangesAsync();
            Assert.True(true);
        }
    }
}
