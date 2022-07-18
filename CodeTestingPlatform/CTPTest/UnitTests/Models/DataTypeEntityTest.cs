using CodeTestingPlatform.DatabaseEntities.Local;
using CTPTest.Helpers;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Models {
    public class DataTypeEntityTest {
        private readonly TestSetup _testSetup = new();

        public DataTypeEntityTest() {
        }
        /*
        [Fact]
        public async Task Test_GetDataTypesAsync() {
            using var context = _testSetup.CreateContext();
            DataType dataType = new(context);

            List<DataType> types = await dataType.GetDataTypesAsync(8);

            Assert.Equal(13, types.Count());
        }
        [Fact]
        public async Task Test_GetDataType() {
            using var context = _testSetup.CreateContext();
            DataType _dataType = new(context);
            DataType dt = await _dataType.GetDataType(16);
            Assert.Equal("int", dt.DataType1);
            Assert.Equal(34, dt.LanguageId);
            Assert.Equal(16, dt.DataTypeId);
            Assert.NotNull(dt.Language);
            Assert.Empty(dt.SignatureParameters); // should not be included 
        }
        */
    }
}
