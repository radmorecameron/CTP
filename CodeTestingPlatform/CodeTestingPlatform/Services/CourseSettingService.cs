using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public class CourseSettingService : ICourseSettingService {
        private readonly ICourseSettingRepository _courseSettingRepository;

        public CourseSettingService(ICourseSettingRepository courseSettingRepository) {
            _courseSettingRepository = courseSettingRepository;
        }

        public async Task CreateAsync(CourseSetting courseSetting) {
            await _courseSettingRepository.CreateAsync(courseSetting);
        }

        public async Task<CourseSetting> FindByCodeAsync(string courseCode) {
            return await _courseSettingRepository.FindByCodeAsync(courseCode);
        }

        public async Task<List<CourseSetting>> ListAsync() {
            return await _courseSettingRepository.ListAsync();
        }

        public async Task UpdateAsync(CourseSetting courseSetting) {
            await _courseSettingRepository.UpdateAsync(courseSetting);
        }

        public async Task<int> GetDefaultLanguageId(string courseCode) {
            var courseSetting = await FindByCodeAsync(courseCode);

            return (courseSetting != null) ? courseSetting.DefaultLanguageId : 0;
        }
    }
}
