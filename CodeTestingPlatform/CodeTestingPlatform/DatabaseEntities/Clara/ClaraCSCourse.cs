using CodeTestingPlatform.DatabaseEntities.Local;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.DatabaseEntities.Clara {
    public class ClaraCSCourse {
        private readonly ClaraTeacherCourse _claraTeacher;
        private readonly ClaraStudentCourse _claraStudent;

        public ClaraCSCourse() {
            _claraStudent = new ClaraStudentCourse();
            _claraTeacher = new ClaraTeacherCourse();
        }

        public string CourseCode { get; set; }

        public string CourseName { get; set; }
        [Key]
        public string CSCoursesId { get; set; }
        /*
        public async Task<List<Course>> GetTeacherCSCourses(int teacherId, int semesterId) {
            List<ClaraTeacherCourse> teacherCourses = await _claraTeacher.GetClaraTeacherCourses(teacherId, semesterId);
            List<Course> courseList = new();

            foreach (ClaraTeacherCourse course in teacherCourses) {
                courseList.Add(await _db.ClaraCSCourses.Where(c => c.CourseCode == course.CourseCode)
                                                       .Select(c => new Course(c.CourseName, c.CourseCode))
                                                       .FirstOrDefaultAsync());
            }

            return courseList;
        }

        public async Task<List<Course>> GetStudentCSCourses(int studentId, int semesterId) {
            List<ClaraStudentCourse> studentCourses = await _claraStudent.GetClaraStudentCourses(studentId, semesterId);
            List<Course> courseList = new();

            foreach (ClaraStudentCourse course in studentCourses) {
                courseList.Add(await _db.ClaraCSCourses.Where(c => c.CourseCode == course.CourseCode)
                                                       .Select(c => new Course(c.CourseName, c.CourseCode))
                                                       .FirstOrDefaultAsync());
            }

            return courseList;
        }
        */
    }
}
