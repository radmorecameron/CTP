using CodeTestingPlatform.DatabaseEntities.Clara;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services.Interfaces {
    public interface IClaraService {
        Task<List<ClaraCSStudent>> GetClaraCSStudents();
    }
}
