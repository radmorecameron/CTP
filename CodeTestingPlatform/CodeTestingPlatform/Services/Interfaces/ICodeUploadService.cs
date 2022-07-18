using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services.Interfaces {
    public interface ICodeUploadService {
        Task<CodeUpload> FindByIdAsync(int codeUploadId);

        Task CreateAsync(CodeUpload codeUpload);

        Task DeleteAsync(CodeUpload codeUpload);

        Task UpdateAsync(CodeUpload codeUpload);
    }
}
