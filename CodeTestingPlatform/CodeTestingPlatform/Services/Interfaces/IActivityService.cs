using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services.Interfaces {
    public interface IActivityService {
        Task<Activity> FindByIdAsync(int id);
        Task<IList<Activity>> ListAsync(int? courseId = null);
        Task<IPagedList<Activity>> ListAsync(int pageIndex, int pageSize, int? courseId = null);
        Task CreateAsync(Activity activity);
        Task UpdateAsync(Activity activity);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Dictionary<int, List<string>> FindInvalidTestCases(IEnumerable<MethodSignature> methodSignatures, out Dictionary<int, int> invalidSignatures);
        }
}
