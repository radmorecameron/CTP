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
    [Authorize(Roles = "TE")]
    [SessionTimeout]
    public class TeacherController : Controller {
        private readonly ICurrentSession _currentSession;
        private readonly ITeacherService _teacherService;
        private readonly ICourseService _courseService;
        public TeacherController(ICurrentSession currentSession, ITeacherService teacherService, ICourseService courseService) {
            _currentSession = currentSession;
            _courseService = courseService;
            _teacherService = teacherService;
        }
        
        public async Task<IActionResult> Index() {
            var teacher = await _teacherService.FindByIdAsync(_currentSession.GetEmployeeId());
            var courses = await _courseService.ListAsync(teacher.UserId);
            return View(courses);
        }
    }
}
