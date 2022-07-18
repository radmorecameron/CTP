using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories {
    public class CourseSettingRepository : GenericRepository<CourseSetting>, ICourseSettingRepository {
        public CourseSettingRepository(CTP_TESTContext context) : base(context) {

        }

        public async Task CreateAsync(CourseSetting courseSetting) {
            _context.CourseSettings.Add(courseSetting);
            await _context.SaveChangesAsync();
        }

        public async Task<CourseSetting> FindByCodeAsync(string courseCode) {
            return await _context.CourseSettings
                .FirstOrDefaultAsync(c => c.CourseCode == courseCode);
        }

        public async Task<List<CourseSetting>> ListAsync() {
            return await _context.CourseSettings
                .ToListAsync();
        }

        public async Task UpdateAsync(CourseSetting courseSetting) {
            _context.CourseSettings.Update(courseSetting);
            await _context.SaveChangesAsync();
        }
    }
}
