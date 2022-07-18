using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories;
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
    public class MethodSignatureRepoTest {
        public readonly TestSetup ts;
        public MethodSignatureRepoTest() {
            ts = new();
        }
        [Fact]
        public async Task MethodSignature_ListAsyncWithActivityId() {
            using var context = ts.CreateContext();
            MethodSignatureRepository _methodSignatureRepository = new(context);
            List<MethodSignature> _signatures = await _methodSignatureRepository.ListAsync(1);
            Assert.Equal("get_evens", _signatures.ElementAt(0).MethodName);
            Assert.Equal("get_evens_count", _signatures.ElementAt(1).MethodName);
        }
        [Fact]
        public async Task MethodSignature_ListAsyncWithoutActivityId() {
            using var context = ts.CreateContext();
            MethodSignatureRepository _methodSignatureRepository = new(context);
            List<MethodSignature> _signatures = await _methodSignatureRepository.ListAsync(null);
            Assert.Equal("get_evens", _signatures.ElementAt(0).MethodName);
            Assert.Equal("get_evens_count", _signatures.ElementAt(1).MethodName);
        }
        [Fact]
        public async Task MethodSignature_FindByIdValid() {
            using var context = ts.CreateContext();
            MethodSignatureRepository _methodSignatureRepository = new(context);
            MethodSignature sig = await _methodSignatureRepository.FindByIdAsync(1);
            Assert.Equal("get_evens", sig.MethodName);
        }
        [Fact]
        public async Task MethodSignature_FindByIdInvalid() {
            using var context = ts.CreateContext();
            MethodSignatureRepository _methodSignatureRepository = new(context);
            MethodSignature sig = await _methodSignatureRepository.FindByIdAsync(-1);
            Assert.Null(sig);
        }
        [Fact]
        public async Task MethodSignature_CreateAsyncValid() {
            using var context = ts.CreateContext();
            MethodSignature ms = new() { 
                MethodName = "Test",
                ActivityId = 1,
                Description = "Do cool stuff",
                ReturnTypeId = 1
            };
           
            MethodSignatureRepository _methodSignatureRepository = new(context);
            try {
                await _methodSignatureRepository.CreateAsync(ms);
                Assert.True(true);
            } catch (System.Exception) {
                Assert.True(false);
            }
        }
        [Fact]
        public async Task MethodSignature_CreateAsyncInvalid() {
            using var context = ts.CreateContext();
            MethodSignature ms = new() {
                MethodName = "Test",
                Description = "Do cool stuff",
                ReturnTypeId = 1
            };
            ms.SignatureParameters = new List<SignatureParameter> {
                new SignatureParameter() {
                    MethodSignatureId = ms.SignatureId,
                    DataTypeId = 16,
                    InputParameter = true,
                    ParameterPosition = 0,
                    RequiredParameter = true,
                    ParameterName = "param",
                    DefaultValue = "6",
                }
            };
            MethodSignatureRepository _methodSignatureRepository = new(context);
            try {
                await _methodSignatureRepository.CreateAsync(ms);
                Assert.True(false);
            } catch (System.Exception) {
                Assert.True(true);
            }
        }
        [Fact]
        public async Task MethodSignature_Update_Remove_Params() {
            using var context = ts.CreateContext();
            MethodSignatureRepository _methodSignatureRepository = new(context);
            MethodSignature ms = await _methodSignatureRepository.FindByIdAsync(1);
            ms.ReturnTypeId = 7;
            ms.SignatureParameters = new List<SignatureParameter> {
                ms.SignatureParameters.ElementAt(0),
                new SignatureParameter {
                    MethodSignatureId = 7,
                    DataTypeId = 4,
                    InputParameter = true,
                    ParameterPosition = 1,
                    ParameterName = "paramz",
                    RequiredParameter = true
                }
            };
            try {
                await _methodSignatureRepository.UpdateAsync(ms);
                Assert.True(true);
            } catch (System.Exception e) {
                Assert.Equal(new System.Exception(), e);
            }
        }
        [Fact]
        public async Task MethodSignature_DeleteAsync() {
            using var context = ts.CreateContext();
            MethodSignatureRepository _methodSignatureRepository = new(context);
            MethodSignature ms = new() {
                MethodName = "Test",
                Description = "Do cool stuff",
                ReturnTypeId = 1,
                ActivityId = 1
            };
            try {
                await _methodSignatureRepository.CreateAsync(ms);
                await _methodSignatureRepository.DeleteAsync(ms);
                Assert.True(true);
            } catch (System.Exception e) {
                Assert.Equal(new System.Exception(), e);
                Assert.True(false);
            }
        }
    }
}
