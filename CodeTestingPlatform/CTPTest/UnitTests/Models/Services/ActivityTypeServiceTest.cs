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
    public class ActivityTypeServiceTest {
        private readonly TestSetup ts;
        public ActivityTypeServiceTest() {
            ts = new();
        }
        public IActivityTypeService CreateActivityTypeService() {
            CTP_TESTContext ctx = ts.CreateContext();
            IActivityTypeRepository iatr = new ActivityTypeRepository(ctx);
            IActivityTypeService iats = new ActivityTypeService(iatr);
            return iats;
        }
        [Fact]
        public async Task FindByAsync() {
            IActivityTypeService atService = CreateActivityTypeService();
            ActivityType at = await atService.FindByIdAsync(1);
            Assert.NotNull(at);
        }
        [Fact]
        public async Task ListAsync() {
            IActivityTypeService atService = CreateActivityTypeService();
            IList<ActivityType> a = await atService.ListAsync();
            Assert.NotEmpty(a);
        }

    }
}
