using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services.Interfaces {
    public interface IActivityTypeService {
        Task<ActivityType> FindByIdAsync(int id);
        Task<IList<ActivityType>> ListAsync();
        //Task CreateAsync(ActivityType activity);
        //Task UpdateAsync(ActivityType activity);
        //Task DeleteAsync(ActivityType activity);
        //Task<bool> ExistsAsync(int id);
    }
}
