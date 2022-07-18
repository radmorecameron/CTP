using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Repositories;
using CTPTest.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Models.repository {
    [ExcludeFromCodeCoverage]
    public class ActivityRepoTest {
        public readonly TestSetup ts;
        public ActivityRepoTest() {
            ts = new();
        }

        [Fact]
        public async Task Activity_ListQuery() {
            using var context = ts.CreateContext();
            ActivityRepository _activityRepository = new(context);

            var activities = await _activityRepository.GetAllAsync(
                predicate: a => a.ActivityId == 1,
                include: source => source.Include(a => a.Course).Include(a => a.Language).
                                          Include(a => a.MethodSignatures).ThenInclude(s => s.ReturnType),
                orderBy: q => q.OrderBy(a => a.ActivityId));


            Assert.Equal("Lab 2", activities.ElementAt(0).Title);

            var activity = activities.ElementAt(0);

            Assert.NotNull(activity.Course);
            Assert.NotNull(activity.Language);
            Assert.NotNull(activity.MethodSignatures);
            Assert.NotNull(activity.MethodSignatures.ElementAt(0).ReturnType);
        }

        [Fact]
        public async Task Activity_ListAsyncWithPageable() {
            using var context = ts.CreateContext();
            ActivityRepository _activityRepository = new(context);

            var activities = await _activityRepository.GetAllAsync(
                predicate: a => a.ActivityId == 1, // Filter 
                orderBy: q => q.OrderBy(a => a.ActivityId), // Sort by ActivityId
                include: q => q.Include(a => a.Course).Include(a => a.Language).Include(a => a.MethodSignatures)); // Include Course, Languange, 

            Assert.Equal("Lab 2", activities.ElementAt(0).Title);

            var activity = activities.ElementAt(0);

            Assert.NotNull(activity.Course);
            Assert.NotNull(activity.Language);
            Assert.NotNull(activity.MethodSignatures);
        }

        [Fact]
        public async Task Activity_ListAsyncWithoutPageable() {
            using var context = ts.CreateContext();
            ActivityRepository _activityRepository = new(context);

            var activities = await _activityRepository.GetAllAsync(
                a=>a.ActivityId == 1, // Filter 
                q=>q.OrderBy(a=>a.ActivityId), // Sort by ActivityId
                q => q.Include(a => a.Course).Include(a => a.Language).Include(a => a.MethodSignatures)); // Include Course, Languange, 

            Assert.Equal("Lab 2", activities.ElementAt(0).Title);

            var activity = activities.ElementAt(0);

            Assert.NotNull(activity.Course);
            Assert.NotNull(activity.Language);
            Assert.NotNull(activity.MethodSignatures);
        }

        [Fact]
        public async Task Activity_ListAsyncWithActivityId() {
            using var context = ts.CreateContext();
            ActivityRepository _activityRepository = new(context);
            IEnumerable<Activity> _activities = await _activityRepository.GetAllAsync(predicate: a=> a.ActivityId == 1);
            Assert.Equal("Lab 2", _activities.ElementAt(0).Title);
        }

        [Fact]
        public async Task Activity_ListAsyncWithoutActivityId() {
            using var context = ts.CreateContext();
            ActivityRepository _activityRepository = new(context);
            IEnumerable<Activity> _activities = await _activityRepository.GetAllAsync();
            Assert.Equal("Lab 2", _activities.ElementAt(0).Title);
        }

        [Fact]
        public async Task Activity_FindByIdValid() {
            using var context = ts.CreateContext();
            ActivityRepository _activityRepository = new(context);
            Activity act = await _activityRepository.FindOneAsync(t => t.ActivityId == 1);
            Assert.Equal("Lab 2", act.Title);
        }

        [Fact]
        public async Task Activity_FindByIdInvalid() {
            using var context = ts.CreateContext();
            ActivityRepository _activityRepository = new(context);
            Activity act = await _activityRepository.FindOneAsync(t => t.ActivityId == -1);
            Assert.Null(act);
        }

        [Fact]
        public async Task Activity_CreateAsyncValid() {
            using var context = ts.CreateContext();
            Activity act = new() {
                ActivityId = 2,
                Title = "TestActivity",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                ActivityTypeId = 2,
                CourseId = 1,
                LanguageId = 38
            };

            ActivityRepository _activityRepository = new(context);
            try {
                _activityRepository.Add(act);
                await _activityRepository.SaveChangesAsync();
                Assert.True(true);
            } catch (System.Exception) {
                Assert.True(false);
            }
        }

        [Fact]
        public async Task Activity_CreateAsyncInvalid() {
            using var context = ts.CreateContext();
            Activity act = new() {
                ActivityId = 2,
                Title = "TestActivity",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                ActivityTypeId = 2,
                CourseId = 1,
                LanguageId = 38
            };
            act.MethodSignatures = new List<MethodSignature> {
                new MethodSignature() {
                    MethodName = "Test",
                    Description = "Wow so cool",
                    ReturnTypeId = 1
                }
            };

            ActivityRepository _activityRepository = new(context);
            try {
                 _activityRepository.Add(act);
                await _activityRepository.SaveChangesAsync();
                Assert.True(false);
            } catch (System.Exception) {
                Assert.True(true);
            }
        }

        [Fact]
        public async Task Activity_Update_Change_ActivityTypeId() {
            using var context = ts.CreateContext();
            ActivityRepository _activityRepository = new(context);
            Activity act = await _activityRepository.FindOneAsync(t => t.ActivityId == 1);
            act.ActivityTypeId = 2;

            try {
                _activityRepository.Update(act);
                await _activityRepository.SaveChangesAsync();
                Assert.True(true);
            } catch (System.Exception e) {
                Assert.Equal(new System.Exception(), e);
            }
        }

        [Fact]
        public async Task Activity_DeleteAsync() {
            using var context = ts.CreateContext();
            Activity act = new() {
                ActivityId = 2,
                Title = "TestActivity",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                ActivityTypeId = 2,
                CourseId = 1,
                LanguageId = 38
            };

            act.MethodSignatures = new List<MethodSignature> {
                new MethodSignature() {
                    MethodName = "Test",
                    Description = "Wow so cool",
                    ReturnTypeId = 1
                }
            };

            ActivityRepository _activityRepository = new(context);
            try {
                _activityRepository.Add(act);
                await _activityRepository.DeleteAsync(act.ActivityId);
                await _activityRepository.SaveChangesAsync();
                Assert.True(true);
            } catch (System.Exception e) {
                Assert.Equal(new System.Exception(), e);
                Assert.True(false);
            }
        }
    }
}
