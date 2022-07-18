using CodeTestingPlatform.DatabaseEntities.Clara;
using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories.Interfaces {
    public interface IClaraRepository {
        Task<List<ClaraTeacherCourse>> GetClaraTeacherCourses(int teacherId, int semesterId);

        Task<List<ClaraStudentCourse>> GetClaraStudentCourses(int studentId, int semesterId);

        Task<List<ClaraCSStudent>> GetClaraCSStudents();

        Task<List<Course>> GetTeacherCSCourses(int teacherId, int semesterId);

        Task<List<Course>> GetStudentCSCourses(int studentId, int semesterId);
    }
}
