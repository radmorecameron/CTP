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
    public class ActivityServiceTest {
        private readonly TestSetup ts;
        public ActivityServiceTest() {
            ts = new();
        }
        public IActivityService CreateActivityService() {
            CTP_TESTContext ctx = ts.CreateContext();
            IActivityRepository iar = new ActivityRepository(ctx);
            IActivityService ias = new ActivityService(iar);
            return ias;
        }
        [Fact]
        public async Task FindByAsync() {
            IActivityService activityService = CreateActivityService();
            Activity a = await activityService.FindByIdAsync(1);
            Assert.NotNull(a);
        }
        [Fact]
        public async Task ListAsync_NoCourse() {
            IActivityService activityService = CreateActivityService();
            IList<Activity> a = await activityService.ListAsync();
            Assert.NotEmpty(a);
        }
        [Fact]
        public async Task ListAsync_WithCourse() {
            IActivityService activityService = CreateActivityService();
            IList<Activity> a = await activityService.ListAsync(1);
            Assert.NotEmpty(a);
        }
        [Fact]
        public async Task ListAsync_Paging_NoCourse() {
            IActivityService activityService = CreateActivityService();
            IPagedList<Activity> a = await activityService.ListAsync(0, 1);
            Assert.NotEmpty(a.Items);
        }
        [Fact]
        public async Task ListAsync_Paging_Course() {
            IActivityService activityService = CreateActivityService();
            IPagedList<Activity> a = await activityService.ListAsync(0, 1, 1);
            Assert.NotEmpty(a.Items);
        }
        [Fact]
        public async Task CreateAsync() {
            IActivityService activityService = CreateActivityService();
            Activity a = new() {
                ActivityTypeId = 1,
                CourseId = 1,
                LanguageId = 38,
                Title = "",
                StartDate = DateTime.Now
            };
            await activityService.CreateAsync(a);
            Assert.True(true);
        }
        [Fact]
        public async Task UpdateAsync() {
            IActivityService activityService = CreateActivityService();
            Activity a = await activityService.FindByIdAsync(1);
            a.Title = "Joe mama";
            await activityService.UpdateAsync(a);
            Assert.True(true);
        }
        [Fact]
        public async Task DeleteAsync() {
            IActivityService activityService = CreateActivityService();
            await activityService.DeleteAsync(1);
            Assert.True(true);
        }
        [Fact]
        public async Task ExistsAsync() {
            IActivityService activityService = CreateActivityService();
            bool exists = await activityService.ExistsAsync(1);
            Assert.True(exists);
        }
        [Fact]
        public async Task Demo() {
            CTP_TESTContext ctx = ts.CreateContext();
            IActivityRepository iar = new ActivityRepository(ctx);
            ActivityService activityService = new(iar);
            IPagedList<Activity> a = await activityService.Demo(0, 1);
            Assert.NotEmpty(a.Items);
        }
    }
}
