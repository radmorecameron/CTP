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
    public class DataTypeServiceTest {
        private readonly TestSetup ts;
        public DataTypeServiceTest() {
            ts = new();
        }
        public IDataTypeService CreateDataTypeService() {
            CTP_TESTContext ctx = ts.CreateContext();
            IDataTypeRepository idtr = new DataTypeRepository(ctx);
            IDataTypeService idts = new DataTypeService(idtr);
            return idts;
        }
        [Fact]
        public async Task FindByAsync() {
            IDataTypeService dtService = CreateDataTypeService();
            DataType dt = await dtService.FindByIdAsync(1);
            Assert.NotNull(dt);
        }
        [Fact]
        public async Task ListAsync() {
            IDataTypeService dtService = CreateDataTypeService();
            IList<DataType> dt = await dtService.ListAsync(38);
            Assert.NotEmpty(dt);
        }
        [Fact]
        public async Task ExistsAsync() {
            IDataTypeService dtService = CreateDataTypeService();
            bool exists = await dtService.ExistsAsync(1);
            Assert.True(exists);
        }
    }
}
