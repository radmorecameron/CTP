using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CTPTest.Helpers;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Models {
    public class TestCaseTest {
        public readonly TestSetup ts;
        public TestCaseTest() {
            ts = new();
        }
        /*
        [Fact]
        public async Task TestCase_GetTestCases() {
            using var context = ts.CreateContext();
            var testCaseObj = new TestCase(context);

            var testCases = await testCaseObj.GetTestCases();

            Assert.Equal(10, testCases.Count());
        }

        [Fact]
        public async Task TestCase_GetTestCaseById() {
            using var context = ts.CreateContext();
            var testCaseObj = new TestCase(context);

            var testCase = await testCaseObj.GetTestCase(41);
            Assert.Equal("negatives", testCase.TestCaseName);
        }

        [Fact]
        public async Task TestCase_GetTestCaseById_DoesNot_Exist() {
            using var context = ts.CreateContext();
            var testCaseObj = new TestCase(context);

            var testCase = await testCaseObj.GetTestCase(69);
            Assert.Null(testCase);
        }

        [Fact]
        public async Task TestCase_Add_Success() {
            using var context = ts.CreateContext();
            TestCase _testCase = new(context) {
                TestCaseId = 69420,
                MethodSignatureId = 91,
                TestCaseName = "UnitTest",
                ExpectedValue = "idk"
            };
            try {
                await _testCase.AddTestCase();
                Assert.True(true);
            } catch (Exception) {
                Assert.True(false);
            }
        }

        [Fact]
        public async Task TestCase_Add_Fail() {
            using var context = ts.CreateContext();
            TestCase _testCase = new(context) {
                TestCaseId = 41,
                MethodSignatureId = 91,
                TestCaseName = "UnitTest",
                ExpectedValue = "idk"
            };
            try {
                await _testCase.AddTestCase();
                Assert.True(false);
            } catch (Exception) {
                Assert.True(true);
            }
        }

        [Fact]
        public async Task TestCase_Edit() {
            using var context = ts.CreateContext();
            TestCase _testCase = new(context);
            TestCase testCase = await _testCase.GetTestCase(41);
            testCase.TestCaseName = "JamAManOfFortune";
            await testCase.EditTestCase();
            TestCase result = await _testCase.GetTestCase(41);
            Assert.Equal("JamAManOfFortune", result.TestCaseName);
            Assert.Equal(41, result.TestCaseId);
            Assert.Equal(91, result.MethodSignatureId);
        }

        [Fact]
        public async Task TestCase_Remove() {
            using var context = ts.CreateContext();
            TestCase _testCase = new(context) {
                TestCaseId = 420,
                MethodSignatureId = 91,
                TestCaseName = "UnitTest",
                ExpectedValue = "idk"
            };

            await _testCase.AddTestCase();
            try {
                await _testCase.RemoveTestCase();
                TestCase result = await _testCase.GetTestCase(420);
                Assert.Null(result);
            } catch (Exception) {
                Assert.True(false);
            }
        }
        [Fact]
        public async Task TestTestCase_MethodFormat() {
            using var context = ts.CreateContext();
            TestCase _testCase = new(context);
            TestCase tc = await _testCase.GetTestCase(41);
            Assert.Equal("Average(-4, -7)", tc.MethodFormat());
        }
        */
    }
}
