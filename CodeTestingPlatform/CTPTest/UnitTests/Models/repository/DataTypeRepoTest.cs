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
    public class DataTypeRepoTest {
        public readonly TestSetup ts;
        public DataTypeRepoTest() {
            ts = new();
        }
        [Fact]
        public async Task DataType_ListAsyncWithLanguageId() {
            using var context = ts.CreateContext();
            DataTypeRepository _dt = new(context);
            IList<DataType> _dts = await _dt.GetAllAsync(predicate: t=> t.LanguageId == 38);
            Assert.Equal("str", _dts[0].DataType1);
            Assert.Equal("int", _dts[1].DataType1);
        }
        [Fact]
        public async Task DataType_ListAsyncWithoutLanguageId() {
            using var context = ts.CreateContext();
            DataTypeRepository _dt = new(context);
            IList<DataType> _dts = await _dt.GetAllAsync();
            Assert.Equal("str", _dts[0].DataType1);
            Assert.Equal("int", _dts[1].DataType1);
        }
        [Fact]
        public async Task DataType_ExistsTrue() {
            using var context = ts.CreateContext();
            DataTypeRepository _dt = new(context);
            Assert.True(await _dt.ExistsAsync(t=>t.DataTypeId == 1));
        }
        [Fact]
        public async Task DataType_ExistsFalse() {
            using var context = ts.CreateContext();
            DataTypeRepository _dt = new(context);
            Assert.False(await _dt.ExistsAsync(t => t.DataTypeId == -1));
        }
        [Fact]
        public async Task DataType_FindAsyncNotNull() {
            using var context = ts.CreateContext();
            DataTypeRepository _dt = new(context);
            Assert.NotNull(await _dt.FindOneAsync(t=>t.DataTypeId == 1));
        }
        [Fact]
        public async Task DataType_FindAsyncNull() {
            using var context = ts.CreateContext();
            DataTypeRepository _dt = new(context);
            Assert.Null(await _dt.FindOneAsync(t => t.DataTypeId == -1));
        }
    }
}
