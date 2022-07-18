using CodeTestingPlatform.DatabaseEntities.Clara;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Models.Validation;
using CodeTestingPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Controllers {
    [Authorize(Roles = "TE,ST")]
    [SessionTimeout]
    public class CourseController : Controller {
        private readonly ICurrentSession _currentSession;
        private readonly ICourseService _courseService;
        private readonly ICourseSettingService _courseSettingService;
        private readonly ILanguageService _languageService;
        private readonly ITestCaseService _testCaseService;

        public CourseController(ICurrentSession currentSession, ICourseService courseService, ICourseSettingService courseSettingService, ILanguageService languageService, ITestCaseService testCaseService) {
            _currentSession = currentSession;
            _courseService = courseService;
            _testCaseService = testCaseService;
            _courseSettingService = courseSettingService;
            _languageService = languageService;
        }

        [Authorize(Roles = "TE")]
        public async Task<IActionResult> Index() {
            int teacherId = _currentSession.GetEmployeeId();
            List<Course> courses = await _courseService.ListAsync(teacherId, Convert.ToInt32(_currentSession.GetSemesterId()), true);

            return View(courses);
        }

        public async Task<IActionResult> Details(int id) {            
            Course course = await _courseService.FindByIdAsync(id);
            
            if (course == null)
                return NotFound();

            if (_currentSession.IsUserATeacher()) {
                ViewData["languages"] = await _languageService.ListAsync();
                ViewData["courseCode"] = course.CourseCode;
                ViewData["courseId"] = course.CourseId;
                ViewData["defaultLanguageId"] = await _courseSettingService.GetDefaultLanguageId(course.CourseCode);
                ViewData["isTeacher"] = true;
                ViewData["defaultValue"] = "                       ";
            } else {
                ViewData["isTeacher"] = false;
            }
           
            InvalidTestCases(course);
            return View(course);
        }

        [Authorize(Roles = "ST")]
        public async Task<IActionResult> StudentDetails(int id) {
            Course course = await _courseService.FindStudentCourse(id);
            return View(course);
        }

        private void InvalidTestCases(Course course) {
            Dictionary<int, int> invalidActivities = new();
            Dictionary<int, int> invalidSignatures = new();
            Dictionary<int, List<string>> invalidTestCases = _courseService.FindInvalidTestCases(course.Activities, out invalidActivities, out invalidSignatures);
            ViewBag.InvalidActivities = invalidActivities;
            ViewBag.InvalidSignatures = invalidSignatures;
            ViewBag.InvalidTestCases = invalidTestCases;
        }
    }
}
