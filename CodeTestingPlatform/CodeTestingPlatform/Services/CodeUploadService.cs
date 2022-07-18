using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public class CodeUploadService : ICodeUploadService {
        private readonly ICodeUploadRepository _repository;
        public CodeUploadService(ICodeUploadRepository repository) {
            _repository = repository;
        }

        public async Task<CodeUpload> FindByIdAsync(int codeUploadId) {
            return await _repository.FindOneAsync(
                predicate: a => a.CodeUploadId == codeUploadId,
                include: q => q.Include(a => a.Student)
                .Include(a => a.Activity)
                .Include(a=>a.Results));
        }

        public async Task<CodeUpload> FindByStudentIdAndActivityIdAsync(int studentId, int activityId) {
            return await _repository.FindOneAsync(
                predicate: a => a.StudentId == studentId && a.ActivityId == activityId,
                include: q => q.Include(a => a.Student)
                                .Include(a => a.Activity)
                                .Include(a => a.Results));
        }

        public async Task CreateAsync(CodeUpload codeUpload) {
            _repository.Add(codeUpload);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(CodeUpload codeUpload) {
            await _repository.DeleteAsync(codeUpload);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(CodeUpload codeUpload) {
            _repository.Update(codeUpload);
            await _repository.SaveChangesAsync();
        }
    }
}
