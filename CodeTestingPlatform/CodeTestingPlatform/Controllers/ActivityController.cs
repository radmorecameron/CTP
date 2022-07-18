using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Models.Validation;
using CodeTestingPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeTestingPlatform.Controllers {
    [Authorize(Roles = "TE")]
    [SessionTimeout]
    public class ActivityController : Controller {

        private readonly IActivityService _activityService;
        private readonly IActivityTypeService _activityTypeService;
        private readonly ICourseService _courseService;
        private readonly ICourseSettingService _courseSettingService;
        private readonly ILanguageService _languageService;
        private readonly IStudentService _studentService;

        public ActivityController(IActivityService activityService,
            IActivityTypeService activityTypeService,
            ICourseService courseService,
            ICourseSettingService courseSettingService,
            ILanguageService languageService,
            IStudentService studentService) {
            _activityService = activityService;
            _activityTypeService = activityTypeService;
            _courseService = courseService;
            _courseSettingService = courseSettingService;
            _languageService = languageService;
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageIndex = 0, int pageSize = 100, int? courseId = null) {

            IList<Activity> items = await _activityService.ListAsync();
            return View(items);

            //var pagedItems = await _activityService.ListAsync(pageIndex, pageSize, courseId);
            //return View(pagedItems);

        }

        [HttpGet]
        public async Task<IActionResult> Details(int id) {
            Activity activity = await _activityService.FindByIdAsync(id);
            if (activity == null)
                return NotFound();

            InvalidTestCases(activity);
            return View(activity);
        }

        [HttpGet]
        public async Task<IActionResult> ActivityResults(int id) {
            Activity activity = await _activityService.FindByIdAsync(id);
            if (activity == null)
                return NotFound();
            List<Ctpuser> studentList = await _studentService.GetStudentsFromCourse(activity.CourseId);
            return View(new ActivityResults(studentList, activity));
        }
        [HttpGet]
        public async Task<IActionResult> Create(int courseId, string from) {
            TempData["Action"] = from;
            await SetActivityViewBagData(courseId);
            var activity = new Activity {
                CourseId = courseId,
                LanguageId = (int)TempData["DefaultLanguageId"]
            };
            return View(activity);

        }

        [HttpPost]
        public async Task<IActionResult> Create(Activity activity) {
            //ViewData["courseId"]
            await SetActivityViewBagData(activity.CourseId);
            if (ModelState.IsValid) {
                await _activityService.CreateAsync(activity);
                TempData["message"] = "Successfully added the activity " + activity.Title;
                return RedirectToAction("Details", "Activity", new { id = activity.ActivityId });
            } else {
                TempData["message"] = string.Join("<br>&nbsp;&nbsp;&nbsp;&nbsp; -", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
            }

            return View("Create", activity);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, string from, int courseId = 0) {
            await SetActivityViewBagData(courseId);
            TempData["Action"] = from;
            return View(await _activityService.FindByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Activity activity) {
            string from = (string)TempData["Action"] ?? "Activity";
            await SetActivityViewBagData();

            if (ModelState.IsValid) {
                await _activityService.UpdateAsync(activity);
                TempData["message"] = "Successfully modified the activity " + activity.Title;
                if (from == "Course")
                    return RedirectToAction("Details", "Course", new { id = activity.CourseId });
                else
                    return RedirectToAction("Details", "Activity", new { id = activity.ActivityId });
            } else {
                TempData["message"] = string.Join("<br>&nbsp;&nbsp;&nbsp;&nbsp; -", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
            }

            return View("Edit", activity);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id) {
            var activity = await _activityService.FindByIdAsync(id);
            SetDeleteWarningMessage(activity);
            return View(activity);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(Activity activity) {
            int courseId = activity.CourseId;
            await _activityService.DeleteAsync(activity.ActivityId);
            return RedirectToAction("Details", "Course", new { id = courseId });

        }

        private async Task SetActivityViewBagData(int courseId = 0) {
            if (courseId == 0) {
                ViewBag.Courses = await _courseService.GetCourseNamesAsync();
                TempData["DefaultLanguageId"] = 0;
            } else {
                var course = await _courseService.FindByIdAsync(courseId);
                ViewBag.CourseName = course.CourseName;
                TempData["DefaultLanguageId"] = await _courseSettingService.GetDefaultLanguageId(course.CourseCode);
            }

            ViewBag.Types = await _activityTypeService.ListAsync();
            ViewBag.Languages = await _languageService.ListAsync();
        }

        private void InvalidTestCases(Activity activity) {
            Dictionary<int, int> invalidSignatures = new();
            Dictionary<int, List<string>> invalidTestCases = _activityService.FindInvalidTestCases(activity.MethodSignatures, out invalidSignatures);
            ViewBag.InvalidSignatures = invalidSignatures;
            ViewBag.InvalidTestCases = invalidTestCases;
        }

        private void SetDeleteWarningMessage(Activity activity) {
            int testCaseCount = 0;

            if (activity.MethodSignatures.Count > 0) {
                foreach (var method in activity.MethodSignatures)
                    testCaseCount += method.TestCases.Count;
                TempData["ShowDeleteWarning"] = true;
                var deleteMessage = $"This Activity has {activity.MethodSignatures.Count} Method Signature{(activity.MethodSignatures.Count > 1 ? "s" : "")} containing {testCaseCount} Test Case{(testCaseCount > 1 ? "s" : "")}.";
                if (activity.CodeUploads.Count > 0)
                    deleteMessage = deleteMessage.Remove(deleteMessage.Length - 1) + $" and {activity.CodeUploads.Count} Code Submission{(activity.CodeUploads.Count > 1 ? "s" : "")}.";
                TempData["DeleteWarning"] = deleteMessage;
            } else {
                TempData["ShowDeleteWarning"] = false;
            }

        }
    }
}
