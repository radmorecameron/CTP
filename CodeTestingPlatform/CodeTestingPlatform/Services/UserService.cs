using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public class UserService : IUserService {
        private readonly IUserRepository _userRepository;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;

        public UserService(IUserRepository userRepository, IStudentService studentService, ITeacherService teacherService) {
            _userRepository = userRepository;
            _studentService = studentService;
            _teacherService = teacherService;
        }

        public async Task CreateAsync(string firstName, string lastName, int id, bool isTeacher) {
            Ctpuser user = new Ctpuser() { FirstName = firstName, LastName = lastName };

            await _userRepository.CreateAsync(user);

            if (isTeacher) {
                await _teacherService.CreateAsync(new Teacher() {UserId = user.UserId, TeacherId= id });
            }
            else {
                await _studentService.CreateAsync(new Student() {UserId=user.UserId, StudentId = id });
            }
        }

        public async Task<Ctpuser> FindByIdAsync(int id) {
            return await _userRepository.FindByIdAsync(id);
        }

        public async Task<bool> ExistsAsync(int id) {
            return await _userRepository.ExistsAsync(id);
        }
    }
}
