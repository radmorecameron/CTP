using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Models.Validation;
using CTPTest.Helpers;
using FluentValidation.TestHelper;
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
    public class ActivityEntityTest {
        private readonly TestSetup _testSetup = new();
        
        public ActivityEntityTest() {
            
        }
        [Fact]
        public void ActivityValidatorTest() {
            Activity a = new() {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2)
            };

            ActivityValidator av = new();
            var result = av.TestValidate(a);

            Assert.True(result.IsValid);

            a.EndDate = DateTime.Now.AddDays(-1);
            var result2 = av.TestValidate(a);
            Assert.False(result2.IsValid);
        }
        /*
        [Fact]
        public async Task TestActivity_GetActivities() {
            using var context = _testSetup.CreateContext();
            Activity activity = new(context);

            List<Activity> activities = await activity.GetActivities();

            Assert.Equal(4, activities.Count());
        }

        [Fact]
        public async Task TestActivity_GetActivitiesByCourseId() {
            using var context = _testSetup.CreateContext();
            Activity activity = new(context);

            List<Activity> activities = await activity.GetActivitiesByCourseId(28);

            Assert.Equal(2, activities.Count());
        }

        [Fact]
        public async Task TestActivity_GetActivity_Exist() {
            using var context = _testSetup.CreateContext();
            Activity _activity = new(context);

            Activity activity = await _activity.GetActivity(48);

            Assert.Equal("Average of two numbers", activity.Title);
        }

        [Fact]
        public async Task TestActivity_GetActivity_DoesNot_Exist() {
            using var context = _testSetup.CreateContext();
            Activity _activity = new(context);

            Activity activity = await _activity.GetActivity(99);
            Assert.Null(activity);
        }

        [Fact]
        public async Task TestActivity_GetRecentActivities() {
            using var context = _testSetup.CreateContext();

            Mock<IDateTimeProvider> fakeTime = new Mock<IDateTimeProvider>();
            fakeTime.Setup(DateTime => DateTime.Now).Returns(new DateTime(2022, 01, 24, 0, 0, 0));

            Activity activity = new(context, fakeTime.Object);

            List<Activity> activities = await activity.GetRecentActivities();

            Assert.Equal(2, activities.Count);
        }

        [Fact]
        public async Task TestActivity_GetRecentActivities_NoActivities() {
            using var context = _testSetup.CreateContext();

            Mock<IDateTimeProvider> fakeTime = new Mock<IDateTimeProvider>();
            fakeTime.Setup(DateTime => DateTime.Now).Returns(new DateTime(2021, 01, 24, 0, 0, 0));

            Activity activity = new(context, fakeTime.Object);

            List<Activity> activities = await activity.GetRecentActivities();

            Assert.Empty(activities);
        }

        [Fact]
        public async Task TestActivity_CreateActivity() {
            using var context = _testSetup.CreateContext();
            Activity _activity = new(context) {
                Title = "Test Activity",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                CourseId = 20,
                ActivityTypeId = 1,
                LanguageId = 8
            };

            await _activity.AddActivity();

            Assert.True(_activity.ActivityId > 0);
        }

        [Fact]
        public async Task TestActivity_UpdateActivity() {
            using var context = _testSetup.CreateContext();
            Activity _activity = new(context);

            Activity activity = await _activity.GetActivity(48);

            string titleBeforeUpdate = activity.Title;

            activity.Title = "Test";

            await activity.UpdateActivity();

            Activity activityResult = await _activity.GetActivity(48);

            Assert.True(activityResult.Title != titleBeforeUpdate);
        }

        [Fact]
        public async Task TestActivity_RemoveActivity() {
            using var context = _testSetup.CreateContext();
            Activity _activity = new(context) {
                Title = "Test Activity",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                CourseId = 20,
                ActivityTypeId = 1,
                LanguageId = 8
            };

            await _activity.AddActivity();

            int id = _activity.ActivityId;

            await _activity.RemoveActivity();

            Assert.Null(await _activity.GetActivity(id));
        }
        */
    }
}
