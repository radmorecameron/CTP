using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories.Interfaces {
    public interface ICourseSettingRepository {   
        Task<CourseSetting> FindByCodeAsync(string courseCode);
        Task<List<CourseSetting>> ListAsync();
        Task CreateAsync(CourseSetting courseSetting);
        Task UpdateAsync(CourseSetting courseSetting);

    }
}
