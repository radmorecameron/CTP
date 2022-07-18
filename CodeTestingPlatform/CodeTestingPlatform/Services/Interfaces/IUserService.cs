using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services.Interfaces {
    public interface IUserService {
        Task<Ctpuser> FindByIdAsync(int id);
        Task CreateAsync(string firstName, string lastName, int id, bool isTeacher);
        Task<bool> ExistsAsync(int id);
    }
}
