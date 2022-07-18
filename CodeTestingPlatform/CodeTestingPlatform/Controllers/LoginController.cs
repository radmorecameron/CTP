using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Controllers { 
    public class LoginController : Controller {
        private readonly ICurrentSession _currentSession;
        private readonly HtmlEncoder _htmlEncoder;
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;

        public LoginController(ICurrentSession currentSession, HtmlEncoder htmlEncoder, ICourseService courseService, IUserService userService, ITeacherService teacherService, IStudentService studentService) {
            _currentSession = currentSession;
            _htmlEncoder = htmlEncoder;

            _userService = userService;
            _courseService = courseService;
            _teacherService = teacherService;
            _studentService = studentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index() {
            if (TempData["Message"] != null) {
                string msg = (string)TempData["Message"];
                ViewBag.Message = msg;
            }
            if (_currentSession.IsAuthorized()) {
                if (_currentSession.IsUserAStudent())
                    return RedirectToAction("Index", "Student");
                else if (_currentSession.IsUserATeacher() || _currentSession.IsUserACoordinator())
                    return RedirectToAction("Index", "Teacher");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(Login login) {
            if (ModelState.IsValid) {
                await _currentSession.Login(login);
                if (_currentSession.IsAuthorized()) {
                    if (_currentSession.IsUserAStudent()) {
                        Student newStudent = new(_currentSession.GetStudentId());
                        if (await _studentService.IsNewStudentAsync(newStudent.StudentId)) {
                            await _userService.CreateAsync(_currentSession.GetFirstName(), _currentSession.GetLastName(), newStudent.StudentId, false);
                            await _courseService.AddClaraCoursesAsync(await _courseService.ListAsync(newStudent.StudentId, Convert.ToInt32(_currentSession.GetSemesterId()),false));
                        }
                        newStudent = await _studentService.FindByIdAsync(newStudent.StudentId);
                        await _courseService.AddStudentCoursesAsync(newStudent, Convert.ToInt32(_currentSession.GetSemesterId()));
                        return RedirectToAction("Index", "Student");
                    } else if (_currentSession.IsUserATeacher()) {
                        Teacher newTeacher = new(_currentSession.GetEmployeeId());
                        if (await _teacherService.IsNewTeacherAsync(newTeacher.TeacherId)) {
                            await _userService.CreateAsync(_currentSession.GetFirstName(), _currentSession.GetLastName(), newTeacher.TeacherId, true);
                            await _courseService.AddClaraCoursesAsync(await _courseService.ListAsync(newTeacher.TeacherId, Convert.ToInt32(_currentSession.GetSemesterId()), true));
                        }
                        newTeacher = await _teacherService.FindByIdAsync(newTeacher.TeacherId);
                        await _courseService.AddTeacherCoursesAsync(newTeacher, Convert.ToInt32(_currentSession.GetSemesterId()));
                        return RedirectToAction("Index", "Teacher");
                    } else {
                        ModelState.AddModelError(string.Empty, "The username or password you have entered is incorrect.");
                        return View();
                    }
                } else {
                    var errorMessage = "The username or password you have entered is incorrect.";
                    if (_currentSession.IsUserAStudent()) {
                        if (!_currentSession.IsCompSciStudent()) {
                            errorMessage = "Only computer science students are allowed to access this application";
                        }
                    }
                    ModelState.AddModelError(string.Empty, errorMessage);
                    return View();
                }
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult AccessDenied() {
            return View();
        }

        [AllowAnonymous]
        [Route("Logout/{msg?}")]
        public IActionResult Logout(bool isSessionExpired = false) {
            _currentSession.Logout();
            if (isSessionExpired) {
                TempData["Message"] = "You have been logged out due to inactivity";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
