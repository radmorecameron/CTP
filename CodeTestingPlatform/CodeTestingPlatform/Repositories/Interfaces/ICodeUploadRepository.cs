using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories.Interfaces {
    public interface ICodeUploadRepository : IGenericRepository<CodeUpload> {
        Task<CodeUpload> FindByIdAsync(int codeUploadId);
        Task<CodeUpload> FindByIdAsync(int studentId, int activityId);
        Task CreateAsync(CodeUpload codeUpload);
        Task UpdateAsync(CodeUpload codeUpload);
    }
}
