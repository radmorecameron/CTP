using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories {
    public class ResultRepository : GenericRepository<Result>, IResultRepository {
        public ResultRepository(CTP_TESTContext context) : base(context) {

        }
    }
}
