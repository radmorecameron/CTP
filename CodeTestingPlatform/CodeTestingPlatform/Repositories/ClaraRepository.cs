using CodeTestingPlatform.DatabaseEntities.Clara;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories {
    public class ClaraRepository : IClaraRepository {
        private readonly CTP_TESTContext _dbContext;

        public ClaraRepository(CTP_TESTContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<List<ClaraTeacherCourse>> GetClaraTeacherCourses(int teacherId, int semesterId) {
            return await _dbContext.ClaraTeacherCourses.Where(c => c.TeacherId == teacherId
                                                    && c.SemesterId == semesterId).ToListAsync();
        }

        public async Task<List<ClaraStudentCourse>> GetClaraStudentCourses(int studentId, int semesterId) {
            return await _dbContext.ClaraStudentCourses.Where(c => c.StudentId == studentId
                                                    && c.SemesterId == semesterId).ToListAsync();
        }

        public async Task<List<ClaraCSStudent>> GetClaraCSStudents() {
            return await _dbContext.ClaraCSStudents.ToListAsync();
        }

        public async Task<List<Course>> GetTeacherCSCourses(int teacherId, int semesterId) {
            List<ClaraTeacherCourse> teacherCourses = await GetClaraTeacherCourses(teacherId, semesterId);
            List<Course> courseList = new();

            foreach (ClaraTeacherCourse course in teacherCourses) {
                courseList.Add(await _dbContext.ClaraCSCourses.Where(c => c.CourseCode == course.CourseCode)
                                                       .Select(c => new Course(c.CourseName, c.CourseCode))
                                                       .FirstOrDefaultAsync());
            }

            return courseList;
        }

        public async Task<List<Course>> GetStudentCSCourses(int studentId, int semesterId) {
            List<ClaraStudentCourse> studentCourses = await GetClaraStudentCourses(studentId, semesterId);
            List<Course> courseList = new();

            foreach (ClaraStudentCourse course in studentCourses) {
                courseList.Add(await _dbContext.ClaraCSCourses.Where(c => c.CourseCode == course.CourseCode)
                                                       .Select(c => new Course(c.CourseName, c.CourseCode))
                                                       .FirstOrDefaultAsync());
            }

            return courseList;
        }
    }
}
