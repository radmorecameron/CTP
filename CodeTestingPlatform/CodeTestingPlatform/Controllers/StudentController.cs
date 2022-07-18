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
    [Authorize(Roles = "ST")]
    [SessionTimeout]
    public class StudentController : Controller {
        private readonly ICurrentSession _currentSession;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        public StudentController(ICurrentSession currentSession, IStudentService studentService, ICourseService courseService) {
            _currentSession = currentSession;
            _studentService = studentService;
            _courseService = courseService;
        }

        public async Task<IActionResult> Index() {
            var student = await _studentService.FindByIdAsync(_currentSession.GetStudentId());
            var courses = await _courseService.ListAsync(student.UserId);
            List<Activity> newActivities = new();
            List<Activity> expiringActivities = new();
            foreach (UserCourse uc in courses) {
                foreach (Activity a in uc.Course.Activities) {
                    if (a.EndDate <= DateTime.Now.AddDays(6) && a.EndDate >= DateTime.Now) {
                        expiringActivities.Add(a);
                    } else if (a.StartDate >= DateTime.Now.AddDays(-5) && a.StartDate <= DateTime.Now) {
                        newActivities.Add(a);
                    }
                }
            }
            ViewBag.expiringActivities = expiringActivities.OrderBy(a=>a.EndDate).ToList();
            ViewBag.newActivities = newActivities.OrderBy(a => a.StartDate).ToList();
            return View(courses);
        }
    }
}
