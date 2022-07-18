using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public interface IResultService {
        Task<Result> FindByIdAsync(int id);
        Task<IList<Result>> ListAsync(int codeUploadId);
        Task<IPagedList<Result>> ListAsync(int pageIndex, int pageSize);
        Task CreateAsync(int codeUploadId, Result result);
        Task CreateAsync(int codeUploadId, params Result[] results);
        Task UpdateAsync(Result activity);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
