using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories {
    public class ActivityTypeRepository : GenericRepository<ActivityType>, IActivityTypeRepository {
        public ActivityTypeRepository(CTP_TESTContext context) : base(context) {

        }
    }
}
