using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public class TeacherService : ITeacherService {
        private readonly ITeacherRepository _teacherRepository;
        public TeacherService(ITeacherRepository teacherRepository) {
            _teacherRepository = teacherRepository;
        }

        public async Task CreateAsync(Teacher teacher) {
            await _teacherRepository.CreateAsync(teacher);
        }

        public async Task<Teacher> FindByIdAsync(int id) {
            return await _teacherRepository.FindByIdAsync(id);
        }

        public async Task<bool> IsNewTeacherAsync(int teacherId) {
            return await _teacherRepository.IsNewTeacherAsync(teacherId);
        }
    }
}
