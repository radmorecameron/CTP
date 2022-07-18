using CodeTestingPlatform.DatabaseEntities.Clara;
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
    public class CourseSettingServiceTest {
        private readonly TestSetup ts;
        public CourseSettingServiceTest() {
            ts = new();
        }
        public ICourseSettingService CreateCourseSettingServiceTest() {
            CTP_TESTContext ctx = ts.CreateContext();
            ICourseSettingRepository icsr = new CourseSettingRepository(ctx);
            ICourseSettingService icss = new CourseSettingService(icsr);
            return icss;
        }
        [Fact]
        public async Task ListAsync() {
            ICourseSettingService cService = CreateCourseSettingServiceTest();
            List<CourseSetting> csList = await cService.ListAsync();
            Assert.NotEmpty(csList);
        }
        [Fact]
        public async Task FindByCodeAsync() {
            ICourseSettingService cService = CreateCourseSettingServiceTest();
            CourseSetting cs = await cService.FindByCodeAsync("420-G30");
            Assert.NotNull(cs);
        }
    }
}
