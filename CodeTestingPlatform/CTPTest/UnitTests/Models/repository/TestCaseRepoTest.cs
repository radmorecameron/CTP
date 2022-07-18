using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories;
using CodeTestingPlatform.Services;
using CTPTest.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Models.repository {
    [ExcludeFromCodeCoverage]
    public class TestCaseRepoTest {
        public readonly TestSetup ts;
        public TestCaseRepoTest() {
            ts = new();
        }
        [Fact]
        public async Task TestCase_CreateAsync() {
            using var context = ts.CreateContext();
            MethodSignatureRepository msr = new(context);
            TestCaseRepository _tcRepo = new(context, msr);
            try {
                await _tcRepo.CreateAsync(new TestCase {
                    TestCaseId = 13,
                    MethodSignatureId = 1,
                    TestCaseName = "New Test Case",
                    ExpectedValue = "[1,2,3]"
                });
                Assert.True(true);
            } catch (System.Exception) {
                Assert.False(true);
            }
        }
        [Fact]
        public async Task TestCase_ExistsAsync() {
            using var context = ts.CreateContext();
            MethodSignatureRepository msr = new(context);
            TestCaseRepository _tcRepo = new(context, msr);
            Assert.True(await _tcRepo.ExistsAsync(1));
        }
        [Fact]
        public async Task TestCase_FindByIdAsync() {
            using var context = ts.CreateContext();
            MethodSignatureRepository msr = new(context);
            TestCaseRepository _tcRepo = new(context, msr);
            TestCase tc = await _tcRepo.FindByIdAsync(1);
            Assert.Equal("Odds and evens", tc.TestCaseName);
            Assert.Equal(1, tc.TestCaseId);
        }
        [Fact]
        public async Task TestCase_ListAsync_NoSignatureId() {
            using var context = ts.CreateContext();
            MethodSignatureRepository msr = new(context);
            TestCaseRepository _tcRepo = new(context, msr);
            List<TestCase> tc = await _tcRepo.ListAsync();
            Assert.Equal(10, tc.Count);
            Assert.Equal("Odds and evens", tc.ElementAt(0).TestCaseName);
            Assert.Equal("All zeros", tc.ElementAt(9).TestCaseName);
        }
        [Fact]
        public async Task TestCase_ListAsync_SignatureId() {
            using var context = ts.CreateContext();
            MethodSignatureRepository msr = new(context);
            TestCaseRepository _tcRepo = new(context, msr);
            List<TestCase> tc = await _tcRepo.ListAsync(1);
            Assert.Equal(5, tc.Count);
            Assert.Equal("Odds and evens", tc.ElementAt(0).TestCaseName);
        }
        [Fact]
        public async Task TestCase_DeleteAsync() {
            using var context = ts.CreateContext();
            MethodSignatureRepository msr = new(context);
            TestCaseRepository _tcRepo = new(context, msr);
            TestCase tc = await _tcRepo.FindByIdAsync(1);
            try {
                await _tcRepo.DeleteAsync(tc);
                Assert.True(true);
            } catch(System.Exception) {
                Assert.False(true);
            }
        }
        [Fact]
        public async Task TestCase_UpdateAsync() {
            using var context = ts.CreateContext();
            MethodSignatureRepository msr = new(context);
            TestCaseRepository _tcRepo = new(context, msr);
            TestCase tc = await _tcRepo.FindByIdAsync(1);
            tc.TestCaseName = "TC1";
            try {
                await _tcRepo.UpdateAsync(tc);
                Assert.True(true);
            } catch (System.Exception) {
                Assert.False(true);
            }
        }
    }
}
