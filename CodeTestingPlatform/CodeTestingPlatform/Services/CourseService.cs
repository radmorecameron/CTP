using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models.Validation;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public class CourseService : ICourseService {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IClaraRepository _claraRepository;

        public CourseService(ICourseRepository courseRepository, IUserRepository userRepository, IStudentRepository studentRepository, ITeacherRepository teacherRepository, IClaraRepository claraRepository) {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _claraRepository = claraRepository;
        }

        public async Task<List<Course>> ListAsync(int userId, int semester, bool isTeacher) {
            List<Course> courseList = new();

            courseList = isTeacher ? await _claraRepository.GetTeacherCSCourses(userId, semester)
                                   : await _claraRepository.GetStudentCSCourses(userId, semester);

            await AddClaraCoursesAsync(courseList);
            return courseList;
        }

        public async Task<List<UserCourse>> ListAsync(int userId) {
            return await _courseRepository.ListAsync(userId);
        }

        public async Task AddClaraCoursesAsync(List<Course> courseList) {
            await _courseRepository.AddClaraCourses(courseList);
        }

        public async Task AddStudentCoursesAsync(Student student, int semesterId) {
            List<Course> courseList = await ListAsync(student.StudentId, semesterId, false);
            Ctpuser user = await _userRepository.FindByIdAsync(student.UserId);
            await AddUserCoursesAsync(user, courseList);
        }

        public async Task AddTeacherCoursesAsync(Teacher teacher, int semesterId) {
            List<Course> courseList = await ListAsync(teacher.TeacherId, semesterId, true);
            Ctpuser user = await _userRepository.FindByIdAsync(teacher.UserId);
            await AddUserCoursesAsync(user, courseList);
        }

        [ExcludeFromCodeCoverageAttribute] // can't test due to being too coupled with Clara
        public async Task AddUserCoursesAsync(Ctpuser user, List<Course> claraCourses) {
            // Check if User currently has courses
            if (user.UserCourses.Count > 0) {
                List<UserCourse> userCourses = user.UserCourses.ToList();

                // Loop through users current courses
                for (int courseCount = userCourses.Count - 1; courseCount >= 0; courseCount--) {
                    UserCourse userCourse = userCourses[courseCount];

                    // Check if User has any clara courses
                    if (claraCourses.Count > 0) {
                        bool unLink = true;
                        for (int claraCount = claraCourses.Count() - 1; claraCount >= 0; claraCount--) { // Loop through user's clara courses
                            Course claraCourse = await _courseRepository.FindByCodeAsync(claraCourses[claraCount].CourseCode);
                            if (userCourse.CourseId == claraCourse.CourseId) {
                                unLink = false;
                            }
                        }
                        if (unLink) {
                            await _courseRepository.RemoveUserCourse(userCourse);
                        }
                    }

                    // If User doesnt have any clara courses, unlink course
                    else {
                        await _courseRepository.RemoveUserCourse(userCourse);
                    }
                }
            }
            foreach (Course tempClaraCourse in claraCourses) {
                Course claraCourse = await _courseRepository.FindByCodeAsync(tempClaraCourse.CourseCode);
                if (!await _courseRepository.IsUserInCourse(user.UserId, claraCourse.CourseId)) {
                    await _courseRepository.AddUserCourse(user.UserId, claraCourse.CourseId);
                }
            }
        }

        public async Task CreateAsync(Course course) {
            await _courseRepository.CreateAsync(course);
        }

        public async Task DeleteAsync(Course course) {
            await _courseRepository.DeleteAsync(course);
        }

        public async Task<Course> FindByCodeAsync(string courseCode) {
            return await _courseRepository.FindByCodeAsync(courseCode);
        }

        public async Task<Course> FindByIdAsync(int id) {
            return await _courseRepository.FindByIdAsync(id);
        }

        public async Task<string> GetCourseNameByIdAsync(int id) {
            return await _courseRepository.GetCourseNameByIdAsync(id);
        }

        public async Task<List<Course>> GetCourseNamesAsync() {
            return await _courseRepository.GetCourseNamesAsync();
        }

        public async Task<bool> IsNewCourseAsync(string courseCode) {
            return await FindByCodeAsync(courseCode) == null;
        }

        //public async Task<List<Course>> ListAsync(int userId, int? semesterId=null, bool? isTeacher=null) {
        //    return await _courseRepository.ListAsync(userId, semesterId, isTeacher);
        //}

        public async Task UpdateAsync(Course course) {
            await _courseRepository.UpdateAsync(course);
        }

        public async Task<bool> ExistsAsync(int id) {
            return await _courseRepository.ExistsAsync(id);
        }

        public async Task<Course> FindStudentCourse(int id) {
            return await _courseRepository.FindStudentCourse(id);
        }

        public Dictionary<int, List<string>> FindInvalidTestCases(IEnumerable<Activity> activities, out Dictionary<int, int> invalidActivities, out Dictionary<int, int> invalidSignatures) {
            invalidActivities = new();
            invalidSignatures = new();
            Dictionary<int, List<string>> invalidTestCases = new();
            foreach (Activity activity in activities) {
                var signatureErrors = 0;
                foreach (MethodSignature signature in activity.MethodSignatures) {
                    var testCaseErrors = 0;
                    foreach (TestCase tc in signature.TestCases) {
                        if (tc.ValidateTestCase) {
                            bool isValid = true;
                            List<string> errorMessages = new();
                            foreach (Parameter param in tc.Parameters) {
                                if (param.Value == null && param.SignatureParameter.RequiredParameter) { // If param is missing and signature parameter is required
                                    isValid = false;
                                    errorMessages.Add($"{param.SignatureParameter.ParameterName}: Missing value for required field. <br> ");
                                } else if (!ValueDataTypeValidator.CheckParamDataType(param.Value, param.SignatureParameter.DataType.DataType1) && param.SignatureParameter.RequiredParameter) { // If Param does not follow correct DataType
                                    isValid = false;
                                    errorMessages.Add($"{param.SignatureParameter.ParameterName}: Doesn't match Data Type ({param.SignatureParameter.DataType.DataType1}). <br> ");
                                }
                            }
                            if (!ValueDataTypeValidator.CheckParamDataType(tc.ExpectedValue, tc.MethodSignature.ReturnType.DataType1)) {
                                isValid = false;
                                errorMessages.Add($"Expected Result: Doesn't match Data Type ({tc.MethodSignature.ReturnType.DataType1}). <br> ");
                            }
                            if (!isValid) {
                                testCaseErrors++;
                                invalidTestCases.Add(tc.TestCaseId, errorMessages);
                            }
                        }
                    }
                    if (testCaseErrors > 0) {
                        signatureErrors++;
                        invalidSignatures.Add(signature.SignatureId, testCaseErrors);
                    }
                }
                if (signatureErrors > 0)
                    invalidActivities.Add(activity.ActivityId, signatureErrors);
            }
            return invalidTestCases;
        }
    }
}
