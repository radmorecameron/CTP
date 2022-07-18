using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models.Validation;
using CodeTestingPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Controllers {
    [Authorize(Roles = "TE")]
    [SessionTimeout]
    public class CourseSettingController : Controller {
        private readonly ICourseSettingService _courseSettingService;

        public CourseSettingController(ICourseSettingService courseSettingService) {
            _courseSettingService = courseSettingService;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDefaultLanguage(int defaultLanguageId, int courseId, string courseCode) {
            var courseSetting = await _courseSettingService.FindByCodeAsync(courseCode);

            if(courseSetting != null) {
                courseSetting.DefaultLanguageId = defaultLanguageId;
                await _courseSettingService.UpdateAsync(courseSetting);
            } else {
                await _courseSettingService.CreateAsync(new CourseSetting {
                    DefaultLanguageId = defaultLanguageId,
                    CourseCode = courseCode
                });
            }
            return RedirectToAction("Details", "Course", new { id = courseId });
        }
    }
}
