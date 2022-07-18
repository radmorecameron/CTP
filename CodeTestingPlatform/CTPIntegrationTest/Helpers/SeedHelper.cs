using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTPIntegrationTest.Helpers {
    public static class SeedHelper {
        public static List<Activity> Activities() {
            return new List<Activity>
            {
                new Activity { ActivityId = 1, Title = "Test1", StartDate = new DateTime(2021, 1, 20), EndDate =  new DateTime(2021, 1, 21), ActivityTypeId = 1, LanguageId = 8, CourseId = 1},
                new Activity { ActivityId = 2, Title = "Test2", StartDate = new DateTime(2021, 1, 20), EndDate =  new DateTime(2021, 1, 21), ActivityTypeId = 1, LanguageId = 8, CourseId = 2},
                new Activity { ActivityId = 3, Title = "Test3", StartDate = new DateTime(2021, 1, 20), EndDate =  new DateTime(2021, 1, 21), ActivityTypeId = 2, LanguageId = 8, CourseId = 3},
                new Activity { ActivityId = 4, Title = "Test4", StartDate = new DateTime(2021, 1, 20), EndDate =  new DateTime(2021, 1, 21), ActivityTypeId = 3, LanguageId = 8, CourseId = 4},
            };
        }

        public static List<ActivityType> ActivityTypes() {
            return new List<ActivityType>
            {
                new ActivityType { ActivityTypeId = 1, ActivityName = "Labs"},
                new ActivityType { ActivityTypeId = 2, ActivityName = "Exercises"},
                new ActivityType { ActivityTypeId = 3, ActivityName = "Assignments"},
            };
        }

        public static List<Course> Courses() {
            return new List<Course>
            {
                new Course {CourseId = 1, CourseName = "Programming III", CourseCode = "420-G30"},
                new Course {CourseId = 2, CourseName = "Web Programming II", CourseCode = "420-H20"},
                new Course {CourseId = 3, CourseName = "Development Project I", CourseCode = "420-K40"},
                new Course {CourseId = 4, CourseName = "Development Project II", CourseCode = "420-E63"},
                new Course {CourseId = 5, CourseName = "Web Programming IV", CourseCode = "420-H40"},
                new Course {CourseId = 6, CourseName = "Networks", CourseCode = "420-F20"},
            };
        }

        public static List<Ctpuser> CTPUsers() {
            return new List<Ctpuser>
            {
                new Ctpuser { UserId = 1, FirstName = "Richard", LastName = "Chan"},
                new Ctpuser { UserId = 2, FirstName = "Ronald", LastName = "Patterson"},
            };
        }

        public static List<DataType> DataTypes() {
            return new List<DataType>
            {
                new DataType { DataTypeId = 1, DataType1 = "string", LanguageId = 8},
                new DataType { DataTypeId = 2, DataType1 = "bool", LanguageId = 8 },
                new DataType { DataTypeId = 3, DataType1 = "int", LanguageId = 8 },
                new DataType { DataTypeId = 4, DataType1 = "char", LanguageId = 8},
            };
        }

        public static List<Language> Languages() {
            return new List<Language>
            {
                new Language { LanguageId = 8, LanguageName = "C#" },
                new Language { LanguageId = 25, LanguageName = "Java" },
                new Language { LanguageId = 26, LanguageName = "JavaScript" },
                new Language { LanguageId = 34, LanguageName = "PHP" },
                new Language { LanguageId = 38, LanguageName = "Python" },
            };
        }

        public static List<Student> Students() {
            return new List<Student>
            {
                new Student { StudentId = 26026, UserId = 3},
                new Student { StudentId = 24472, UserId = 4},
            };
        }

        public static List<Teacher> Teachers() {
            return new List<Teacher>
            {
                new Teacher { TeacherId = 2876, UserId = 1},
                new Teacher { TeacherId = 2203, UserId = 2},
            };
        }

        public static List<UserCourse> UserCourses() {
            return new List<UserCourse>
            {
                new UserCourse { UserCourseId = 1, CourseId = 1, UserId = 1},
                new UserCourse { UserCourseId = 2, CourseId = 2, UserId = 1},
                new UserCourse { UserCourseId = 3, CourseId = 3, UserId = 1},
                new UserCourse { UserCourseId = 4, CourseId = 4, UserId = 2},
                new UserCourse { UserCourseId = 5, CourseId = 5, UserId = 2},
                new UserCourse { UserCourseId = 6, CourseId = 6, UserId = 2},
            };
        }

    }
}
