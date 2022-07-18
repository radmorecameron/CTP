using CodeTestingPlatform.DatabaseEntities.Clara;
using CodeTestingPlatform.DatabaseEntities.Local;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTPIntegrationTest.Helpers {
    public static class DBHelper {
        public static SqliteConnection GetSqliteInMemoryConnection() {
            return new SqliteConnection("Filename=:memory:");
        }

        public static void AddData(CTP_TESTContext context) {
            AddActivityTypes(context);
            AddLanguages(context);
            AddDataTypes(context);
            AddUsers(context);
            AddCourses(context);
            AddStudents(context);
            AddTeachers(context);
            AddUserCourses(context);
            AddActivities(context);
            AddSignatures(context);
            AddSigParams(context);
            AddTestCases(context);
            AddParameters(context);
            AddClaraCSStudent(context);
            AddClaraTeacherCourse(context);
            AddClaraStudentCourse(context);
            AddClaraCSCourse(context);
            AddClaraCSStudent(context);
        }

        public static void AddStudents(CTP_TESTContext context) {
            context.Students.AddRange(
                new Student {
                    StudentId = 24472,
                    UserId = 33,
                },
                new Student {
                    StudentId = 26026,
                    UserId = 30,
                },
                new Student {
                    StudentId = 26231,
                    UserId = 32,
                }
            );
        }
        public static void AddTeachers(CTP_TESTContext context) {
            context.Teachers.AddRange(
                new Teacher { TeacherId = 1388, UserId = 28 },
                new Teacher { TeacherId = 2203, UserId = 22 },
                new Teacher { TeacherId = 2876, UserId = 31 }

            );
            context.SaveChanges();
        }

        public static void AddUsers(CTP_TESTContext context) {
            context.Ctpusers.AddRange(
                new Ctpuser { UserId = 22, FirstName = "Ronald", LastName = "Patterson" },
                new Ctpuser { UserId = 28, FirstName = "Catherine", LastName = "Dufour" },
                new Ctpuser { UserId = 30, FirstName = "Vivian", LastName = "Flora" },
                new Ctpuser { UserId = 31, FirstName = "Cameron", LastName = "Radmore" },
                new Ctpuser { UserId = 32, FirstName = "David", LastName = "Brennan" },
                new Ctpuser { UserId = 33, FirstName = "Carla", LastName = "White" },
                new Ctpuser { UserId = 36 }
            );
        }

        public static void AddActivityTypes(CTP_TESTContext context) {
            context.ActivityTypes.AddRange(
                new ActivityType {
                    ActivityTypeId = 1,
                    ActivityName = "Labs",
                },
                new ActivityType {
                    ActivityTypeId = 2,
                    ActivityName = "Exercises",
                },
                new ActivityType {
                    ActivityTypeId = 3,
                    ActivityName = "Assignments",
                }
            );
        }

        public static void AddCourses(CTP_TESTContext context) {
            context.Courses.AddRange(
                new Course { CourseId = 1, CourseName = "Programming III", CourseCode = "420-G30" },
                new Course { CourseId = 3, CourseName = "Web Programming II", CourseCode = "420-H20" },
                new Course { CourseId = 18, CourseName = "Development Project I", CourseCode = "420-K40" },
                new Course { CourseId = 20, CourseName = "Development Project II", CourseCode = "420-E63" },
                new Course { CourseId = 21, CourseName = "Web Programming IV", CourseCode = "420-H40" },
                new Course { CourseId = 22, CourseName = "Networks", CourseCode = "420-F20" },
                new Course { CourseId = 28, CourseName = "Hardware and Operating Systems", CourseCode = "420-F10" },
                new Course { CourseId = 29, CourseName = "Programming I", CourseCode = "420-H10" }
            );
        }

        public static void AddDataTypes(CTP_TESTContext context) {
            context.AddRange(
                new DataType { DataTypeId = 1, DataType1 = "str", LanguageId = 38 },
                new DataType { DataTypeId = 2, DataType1 = "int", LanguageId = 38 },
                new DataType { DataTypeId = 3, DataType1 = "float", LanguageId = 38 },
                new DataType { DataTypeId = 4, DataType1 = "complex", LanguageId = 38 },
                new DataType { DataTypeId = 5, DataType1 = "bool", LanguageId = 38 },
                new DataType { DataTypeId = 6, DataType1 = "bytes", LanguageId = 38 },
                new DataType { DataTypeId = 7, DataType1 = "bytearray", LanguageId = 38 },
                new DataType { DataTypeId = 8, DataType1 = "frozenset", LanguageId = 38 },
                new DataType { DataTypeId = 9, DataType1 = "list", LanguageId = 38 },
                new DataType { DataTypeId = 10, DataType1 = "set", LanguageId = 38 },
                new DataType { DataTypeId = 11, DataType1 = "dict", LanguageId = 38 },
                new DataType { DataTypeId = 12, DataType1 = "tuple", LanguageId = 38 }
            );
        }

        public static void AddLanguages(CTP_TESTContext context) {
            context.Languages.AddRange(
                new Language { LanguageId = 38, LanguageName = "Python" }
            );
        }

        public static void AddActivities(CTP_TESTContext context) {
            context.Activities.AddRange(
                new Activity { ActivityId = 1, Title = "Lab 2", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(5), ActivityTypeId = 1, CourseId = 1, LanguageId = 38 }
            );
        }

        public static void AddSignatures(CTP_TESTContext context) {
            context.MethodSignatures.AddRange(
                new MethodSignature { SignatureId = 1, MethodName = "get_evens", ActivityId = 1, ReturnTypeId = 9, Description = "Get all even numbers in a list" },
                new MethodSignature { SignatureId = 2, MethodName = "get_evens_count", ActivityId = 1, ReturnTypeId = 2, Description = "Get the # of even values in a list" }
            );
        }

        public static void AddSigParams(CTP_TESTContext context) {
            context.SignatureParameters.AddRange(
                new SignatureParameter { SignatureParameterId = 1, ParameterName = "even_list", ParameterPosition = 0, InputParameter = true, RequiredParameter = true, MethodSignatureId = 1, DataTypeId = 9 },
                new SignatureParameter { SignatureParameterId = 2, ParameterName = "even_list", ParameterPosition = 0, InputParameter = true, RequiredParameter = true, MethodSignatureId = 2, DataTypeId = 9 }
            );
        }
        public static void AddTestCases(CTP_TESTContext context) {
            context.TestCases.AddRange(
                new TestCase { TestCaseId = 1, MethodSignatureId = 1, TestCaseName = "Odds and evens", ExpectedValue = "[2, 4, 6, 8, 10]" },
                new TestCase { TestCaseId = 2, MethodSignatureId = 1, TestCaseName = "Only odds", ExpectedValue = "[]" },
                new TestCase { TestCaseId = 3, MethodSignatureId = 1, TestCaseName = "Negative Numbers", ExpectedValue = "[-2, -4, -200]" },
                new TestCase { TestCaseId = 4, MethodSignatureId = 1, TestCaseName = "Only evens", ExpectedValue = "[100, 200, 300, 400]" },
                new TestCase { TestCaseId = 5, MethodSignatureId = 1, TestCaseName = "Only zeros", ExpectedValue = "[0, 0, 0, 0]" },
                new TestCase { TestCaseId = 6, MethodSignatureId = 2, TestCaseName = "Odds and evens", ExpectedValue = "5" },
                new TestCase { TestCaseId = 7, MethodSignatureId = 2, TestCaseName = "Only odds", ExpectedValue = "0" },
                new TestCase { TestCaseId = 8, MethodSignatureId = 2, TestCaseName = "Negative numbers", ExpectedValue = "3" },
                new TestCase { TestCaseId = 9, MethodSignatureId = 2, TestCaseName = "Only evens", ExpectedValue = "4" },
                new TestCase { TestCaseId = 10, MethodSignatureId = 2, TestCaseName = "All zeros", ExpectedValue = "4" }
            );
        }

        public static void AddParameters(CTP_TESTContext context) {
            context.Parameters.AddRange(
                new Parameter { ParameterId = 1, Value = "[1, 2, 3, 4, 5, 6, 7, 8, 9, 10]", TestCaseId = 1, SignatureParameterId = 1 },
                new Parameter { ParameterId = 2, Value = "[1, 3, 5, 7, 9]", TestCaseId = 2, SignatureParameterId = 1 },
                new Parameter { ParameterId = 3, Value = "[-2, -4, 5, 7, 201, 200]", TestCaseId = 3, SignatureParameterId = 1 },
                new Parameter { ParameterId = 4, Value = "[100, 200, 300, 400]", TestCaseId = 4, SignatureParameterId = 1 },
                new Parameter { ParameterId = 5, Value = "[0, 0, 0, 0]", TestCaseId = 5, SignatureParameterId = 1 },
                new Parameter { ParameterId = 6, Value = "[1, 2, 3, 4, 5, 6, 7, 8, 9, 10]", TestCaseId = 6, SignatureParameterId = 2 },
                new Parameter { ParameterId = 7, Value = "[1, 3, 5, 7, 9]", TestCaseId = 7, SignatureParameterId = 2 },
                new Parameter { ParameterId = 8, Value = "[-2, -4, 5, 7, 201, 200]", TestCaseId = 8, SignatureParameterId = 2 },
                new Parameter { ParameterId = 9, Value = "[100, 200, 300, 400]", TestCaseId = 9, SignatureParameterId = 2 },
                new Parameter { ParameterId = 10, Value = "[0, 0, 0, 0]", TestCaseId = 10, SignatureParameterId = 2 }
            );
        }

        public static void AddUserCourses(CTP_TESTContext context) {
            context.UserCourses.AddRange(
                new UserCourse { UserCourseId = 112, CourseId = 20, UserId = 31 },
                new UserCourse { UserCourseId = 113, CourseId = 21, UserId = 31 }
            );
        }

        public static void AddClaraTeacherCourse(CTP_TESTContext context) {
            context.ClaraTeacherCourses.AddRange(
                new ClaraTeacherCourse { CourseCode = "420F20HR", TeacherId = 2203, SemesterId = 20213, CSTeacherCourseId = Guid.NewGuid().ToString() },
                new ClaraTeacherCourse { CourseCode = "420G40HR", TeacherId = 2876, SemesterId = 20213, CSTeacherCourseId = Guid.NewGuid().ToString() }
            );
        }

        public static void AddClaraStudentCourse(CTP_TESTContext context) {
            context.ClaraStudentCourses.AddRange(
                new ClaraStudentCourse { CourseCode = "420F20HR", StudentId = 26026, SemesterId = 20213, CSStudentCourseId = Guid.NewGuid().ToString() }
            );
        }
        //                 new Ctpuser { UserId = 30, FirstName = "Vivian", LastName = "Flora" },

        public static void AddClaraCSStudent(CTP_TESTContext context) {
            List<ClaraCSStudent> list = context.ClaraCSStudents.ToList();
            context.ClaraCSStudents.AddRange(
                new ClaraCSStudent { FirstName = "Vivian", LastName = "Flora", StudentId = 26026, CSStudentId = Guid.NewGuid().ToString() }
            );
        }
        public static void AddClaraCSCourse(CTP_TESTContext context) {
            context.ClaraCSCourses.AddRange(
                new ClaraCSCourse { CourseCode = "420F20HR", CourseName = "Networks", CSCoursesId = Guid.NewGuid().ToString() },
                new ClaraCSCourse { CourseCode = "420G40HR", CourseName = "Development Projects I", CSCoursesId = Guid.NewGuid().ToString() }
            );
        }
    }
}
