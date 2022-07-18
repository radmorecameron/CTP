using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories {
    public class CodeUploadRepository :  GenericRepository<CodeUpload>, ICodeUploadRepository {
        public CodeUploadRepository(CTP_TESTContext context) : base(context) {

        }

        public async Task<CodeUpload> FindByIdAsync(int codeUploadId) {
            return await _context.CodeUploads
                .Include(a => a.Student)
                .Include(a => a.Activity)
                .FirstOrDefaultAsync(a => a.CodeUploadId == codeUploadId);
        }

        public async Task<CodeUpload> FindByIdAsync(int studentId, int activityId) {
            return await _context.CodeUploads
                .AsNoTracking()
                .Include(a => a.Student)
                .Include(a => a.Activity)
                .FirstOrDefaultAsync(a => a.StudentId == studentId && a.ActivityId == activityId);
        }

        public async Task CreateAsync(CodeUpload codeUpload) {
            _context.CodeUploads.Add(codeUpload);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CodeUpload codeUpload) {
            _context.CodeUploads.Update(codeUpload);
            await _context.SaveChangesAsync();
        }
    }
}
