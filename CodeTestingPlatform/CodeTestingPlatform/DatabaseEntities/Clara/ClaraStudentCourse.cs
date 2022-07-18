using CodeTestingPlatform.DatabaseEntities.Local;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.DatabaseEntities.Clara {
    public class ClaraStudentCourse {
        public string CourseCode { get; set; }
        public int StudentId { get; set; }
        public Int16 SemesterId { get; set; }
        [Key]
        public string CSStudentCourseId { get; set; }

        /*
        public async Task<List<ClaraStudentCourse>> GetClaraStudentCourses(int studentId, int semesterId) {
            return await _db.ClaraStudentCourses.Where(c => c.StudentId == studentId
                                                    && c.SemesterId == semesterId).ToListAsync();
        }
        */
    }
}
