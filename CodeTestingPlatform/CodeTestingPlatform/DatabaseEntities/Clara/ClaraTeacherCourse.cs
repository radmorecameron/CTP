using CodeTestingPlatform.DatabaseEntities.Local;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.DatabaseEntities.Clara {
    public class ClaraTeacherCourse {
        public string CourseCode { get; set; }
        public int TeacherId { get; set; }
        public Int16 SemesterId { get; set; }
        [Key]
        public string CSTeacherCourseId { get; set; }

        /*
        public async Task<List<ClaraTeacherCourse>> GetClaraTeacherCourses(int teacherId, int semesterId) {
            return await _db.ClaraTeacherCourses.Where(c => c.TeacherId == teacherId 
                                                    && c.SemesterId == semesterId).ToListAsync();
        }
        */
    }
}
