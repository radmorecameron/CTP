using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public class StudentService : IStudentService {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository) {
            _studentRepository = studentRepository;
        }

        public async Task CreateAsync(Student student) {
            await _studentRepository.CreateAsync(student);
        }

        public async Task<Student> FindByIdAsync(int id) {
            return await _studentRepository.FindByIdAsync(id);
        }

        public async Task<bool> IsNewStudentAsync(int studentId) {
            return !await ExistsAsync(studentId);
        }

        public async Task<bool> ExistsAsync(int id) {
            return await _studentRepository.ExistsAsync(id);
        }

        public async Task<List<Ctpuser>> GetStudentsFromCourse(int id) {
            return await _studentRepository.GetStudentsFromCourse(id);
        }
    }
}
