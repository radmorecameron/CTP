using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Models.Validation;
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
    public class ParameterEntityTest {
        private readonly TestSetup _testSetup = new();
        /*
        [Fact]
        public async Task TestParameter_Getters() {
            using var context = _testSetup.CreateContext();
            Parameter _param = new(context);
            Parameter param = await _param.GetParameter(39);
            Assert.Equal(39, param.ParameterId);
            Assert.Equal("-4", param.Value);
            Assert.Equal(0, param.ParameterPosition);
            Assert.True(param.InputParameter);
            Assert.Equal(41, param.TestCaseId);
            Assert.Equal(16, param.DataTypeId);
            Assert.True(true);
        }
        [Fact]
        public async Task TestParameter_GetParameters() {
            using var context = _testSetup.CreateContext();
            Parameter _param = new(context);
            List<Parameter> param = await _param.GetParameters();
            Assert.NotEmpty(param);
        }
        [Fact]
        public async Task TestParameter_EditParameter() {
            using var context = _testSetup.CreateContext();
            Parameter _param = new(context);
            Parameter param = await _param.GetParameter(39);
            param.Value = "7";

            try {
                await param.EditParameter();
                Assert.True(true);
            } catch (Exception) {
                Assert.True(false);
            }
        }
        [Fact]
        public void TestParameter_DataTypesValid() {
            using var context = _testSetup.CreateContext();
            Parameter _param = new(context);
            Assert.True(ValueDataTypeValidator.CheckParamDataType("hi", "string"));
            Assert.True(ValueDataTypeValidator.CheckParamDataType("1", "int"));
            Assert.True(ValueDataTypeValidator.CheckParamDataType("true", "bool"));
            Assert.True(ValueDataTypeValidator.CheckParamDataType("x", "char"));
            Assert.True(ValueDataTypeValidator.CheckParamDataType("2.2", "double"));
            Assert.True(ValueDataTypeValidator.CheckParamDataType("29", "long"));
            Assert.True(ValueDataTypeValidator.CheckParamDataType("255", "byte"));
            Assert.True(ValueDataTypeValidator.CheckParamDataType("29", "short"));
            Assert.True(ValueDataTypeValidator.CheckParamDataType("2.2", "float"));
            Assert.True(ValueDataTypeValidator.CheckParamDataType("2022-01-15", "datetime"));
            Assert.True(ValueDataTypeValidator.CheckParamDataType("2.2", "decimal"));
            Assert.True(ValueDataTypeValidator.CheckParamDataType("2.2", "number"));
            Assert.True(ValueDataTypeValidator.CheckParamDataType("[2,5,4]", "int[]"));
            Assert.True(ValueDataTypeValidator.CheckParamDataType("[h,e,h]", "string[]"));
            Assert.True(ValueDataTypeValidator.CheckParamDataType("hi", "default"));
        }

        [Fact]
        public void TestParameter_DataTypesInvalid() {
            using var context = _testSetup.CreateContext();
            Parameter _param = new(context);
            Assert.False(ValueDataTypeValidator.CheckParamDataType("RichardChan", "int"));
            Assert.False(ValueDataTypeValidator.CheckParamDataType("RichardChan", "bool"));
            Assert.False(ValueDataTypeValidator.CheckParamDataType("RichardChan", "char"));
            Assert.False(ValueDataTypeValidator.CheckParamDataType("RichardChan", "double"));
            Assert.False(ValueDataTypeValidator.CheckParamDataType("RichardChan", "long"));
            Assert.False(ValueDataTypeValidator.CheckParamDataType("RichardChan", "byte"));
            Assert.False(ValueDataTypeValidator.CheckParamDataType("RichardChan", "short"));
            Assert.False(ValueDataTypeValidator.CheckParamDataType("RichardChan", "float"));
            Assert.False(ValueDataTypeValidator.CheckParamDataType("RichardChan", "datetime"));
            Assert.False(ValueDataTypeValidator.CheckParamDataType("RichardChan", "decimal"));
            Assert.False(ValueDataTypeValidator.CheckParamDataType("RichardChan", "number"));
            Assert.False(ValueDataTypeValidator.CheckParamDataType("RichardChan", "int[]"));
            Assert.False(ValueDataTypeValidator.CheckParamDataType("RichardChan", "string[]"));
        }
        */
    }
}
