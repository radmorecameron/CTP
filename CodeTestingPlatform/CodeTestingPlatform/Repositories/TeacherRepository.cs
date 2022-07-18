using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeTestingPlatform.Repositories {
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository {
        public TeacherRepository(CTP_TESTContext context) : base(context) {

        }
        public async Task CreateAsync(Teacher teacher) {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task<Teacher> FindByIdAsync(int id) {
            return await _context.Teachers
                          .FirstOrDefaultAsync(t => t.TeacherId == id);
        }

        public async Task<bool> IsNewTeacherAsync(int teacherId) {
            return await FindByIdAsync(teacherId) == null;
        }
    }
}
