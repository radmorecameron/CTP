using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services.Interfaces {
    public interface ICourseSettingService {
        Task<CourseSetting> FindByCodeAsync(string courseCode);
        Task<List<CourseSetting>> ListAsync();
        Task CreateAsync(CourseSetting courseSetting);
        Task UpdateAsync(CourseSetting courseSetting);
        Task<int> GetDefaultLanguageId(string courseCode);
    }
}
