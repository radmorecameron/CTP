using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeTestingPlatform.Repositories {
    public class StudentRepository : GenericRepository<Student>, IStudentRepository {
        public StudentRepository(CTP_TESTContext context) : base(context) {

        }

        public async Task<Student> FindByIdAsync(int id) {
            return await _context.Students
                                .Include(s => s.CodeUploads)
                                .FirstOrDefaultAsync(s => s.StudentId == id);
        }

        public async Task CreateAsync(Student student) {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id) {
            return await _context.Students.AnyAsync(e => e.StudentId == id);
        }

        public async Task<List<Student>> GetStudentsAsync() {
            return await _context.Students.AsNoTracking().ToListAsync();
        }
        public async Task<List<Ctpuser>> GetStudentsFromCourse(int courseId) {
            List<int> allStudentIds = (await GetStudentsAsync()).Select(a=> a.UserId).ToList();
            List<Ctpuser> studentList = await _context.UserCourses
                        .AsNoTracking()
                        .Include(uc => uc.User)
                        .ThenInclude(u => u.Student)
                        .ThenInclude(s => s.CodeUploads)
                        .ThenInclude(cu => cu.Results)
                        .Where(uc => uc.CourseId == courseId)
                        .Where(a => allStudentIds.Contains(a.UserId))
                        .Select(u => u.User)
                        .ToListAsync();
            return studentList;
        }
    }
}
