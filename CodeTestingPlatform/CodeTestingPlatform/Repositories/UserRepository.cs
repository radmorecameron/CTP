using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories {
    public class UserRepository : GenericRepository<Ctpuser>, IUserRepository {
        public UserRepository(CTP_TESTContext context) : base(context) {

        }
        public async Task CreateAsync(Ctpuser user) {
            _context.Ctpusers.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Ctpuser> FindByIdAsync(int id) {
            return await _context.Ctpusers
                             .Include(s => s.UserCourses)
                             .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<bool> IsUserInCourse(int userId, int courseId) {
            UserCourse userCourse = await _context.UserCourses
                            .FirstOrDefaultAsync(c => c.UserId == userId && c.CourseId == courseId);
            return userCourse != null;
        }

        public async Task AddUserToCourseAsync(int userId, int courseId) {
            _context.UserCourses.Add(new UserCourse { UserId = userId, CourseId = courseId });
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id) {
            return await _context.Ctpusers.AnyAsync(e => e.UserId == id);
        }
    }
}
