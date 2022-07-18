using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models.Validation;
using CodeTestingPlatform.Repositories;
using CTPTest.Helpers;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Models {
    public class MethodSignatureEntityTest {
        public readonly TestSetup ts;
        private SignatureValidator validator;
        public MethodSignatureEntityTest() {
            ts = new();
            validator = new();
        }
        [Fact]
        public async Task MethodSignature_MethodFormat() {
            using var context = ts.CreateContext();
            MethodSignatureRepository _methodSignatureRepository = new(context);
            MethodSignature _methodSignature = await _methodSignatureRepository.FindByIdAsync(1);
            Assert.Equal("get_evens(even_list: list)", _methodSignature.MethodFormat());
        }
        [Fact]
        public async Task MethodSignature_TestGetters() {
            using var context = ts.CreateContext();
            MethodSignatureRepository _methodSignatureRepository = new(context);
            MethodSignature _methodSignature = await _methodSignatureRepository.FindByIdAsync(1);
            Assert.NotNull(_methodSignature.Activity);
            Assert.Equal(1, _methodSignature.ActivityId);
            Assert.Equal("Get all even numbers in a list", _methodSignature.Description);
            Assert.NotEmpty(_methodSignature.TestCases);
            Assert.NotNull(_methodSignature.ReturnType);
            Assert.Equal(1, _methodSignature.SignatureId);
            Assert.Equal(9, _methodSignature.ReturnTypeId);
        }
        [Fact]
        public void MethodSignature_Validator() {
            MethodSignature ms = new() {
                MethodName = "Cameron Radmore",
                Description = "123",
                ActivityId = 1,
                ReturnTypeId = 1,
                SignatureId = 22,
            };
            var result = validator.TestValidate(ms);
            Assert.False(result.IsValid);
        }
    }
}
