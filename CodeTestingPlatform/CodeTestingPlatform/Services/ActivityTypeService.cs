using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public class ActivityTypeService : IActivityTypeService {
        private readonly IActivityTypeRepository _activeTypeRepository;
        public ActivityTypeService(IActivityTypeRepository activeTypeRepository) {
            _activeTypeRepository = activeTypeRepository;
        }

        public async Task<ActivityType> FindByIdAsync(int id) {
            return await _activeTypeRepository.FindOneAsync(t => t.ActivityTypeId == id);
        }

        public async Task<IList<ActivityType>> ListAsync() {
            return (await _activeTypeRepository.GetAllAsync());
        }
    }
}
