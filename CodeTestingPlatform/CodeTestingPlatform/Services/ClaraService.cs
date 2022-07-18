using CodeTestingPlatform.DatabaseEntities.Clara;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public class ClaraService : IClaraService {
        private readonly IClaraRepository _claraRepository;
        public ClaraService(IClaraRepository claraRepository) {
            _claraRepository = claraRepository;
        }
        public Task<List<ClaraCSStudent>> GetClaraCSStudents() {
            return _claraRepository.GetClaraCSStudents();
        }
    }
}
