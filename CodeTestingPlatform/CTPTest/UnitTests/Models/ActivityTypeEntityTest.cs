using CodeTestingPlatform.DatabaseEntities.Local;
using CTPTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Models {
    public class ActivityTypeEntityTest {
        public readonly TestSetup ts;
        public ActivityTypeEntityTest() {
            ts = new();
        }
        /*
        [Fact]
        public async Task TestActivityType_GetActivityTypes() {
            using var context = ts.CreateContext();
            ActivityType _activityType = new(context);
            List<ActivityType> activityTypes = await _activityType.GetActivityTypes();
            Assert.Equal(activityTypes.OrderBy(x => x.ActivityName).ToList(), activityTypes.ToList());
        }
        [Fact]
        public async Task TestActivityType_GetActivityType_Exists() {
            using var context = ts.CreateContext();
            ActivityType _activityType = new(context);
            ActivityType activityType = await _activityType.GetActivityType(1);
            Assert.NotNull(activityType);
        }
        [Fact]
        public async Task TestActivityType_GetActivityType_DoesNot_Exist() {
            using var context = ts.CreateContext();
            ActivityType _activityType = new(context);
            ActivityType activityType = await _activityType.GetActivityType(4);
            Assert.Null(activityType);
        }
        */
    }
}
