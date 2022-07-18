using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeTestingPlatform.Repositories {
    public class ActivityRepository : GenericRepository<Activity>, IActivityRepository {

        public ActivityRepository(CTP_TESTContext context) : base(context) {

        }
    }
}
