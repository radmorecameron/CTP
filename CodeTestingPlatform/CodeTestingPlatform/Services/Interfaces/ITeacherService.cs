using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services.Interfaces {
    public interface ITeacherService {
        Task<Teacher> FindByIdAsync(int id);
        Task CreateAsync(Teacher teacher);
        Task<bool> IsNewTeacherAsync(int teacherId);
    }
}
