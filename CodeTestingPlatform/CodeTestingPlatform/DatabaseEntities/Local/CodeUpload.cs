using CodeTestingPlatform.Models;
using CodeTestingPlatform.Models.Extensions;
using CodeTestingPlatform.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

#nullable disable

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class CodeUpload {
        private readonly ICodeUploadRepository _repo;

        public CodeUpload() {
            Results = new HashSet<Result>();
        }

        public CodeUpload(ICodeUploadRepository repo) {
            _repo = repo;
            Results = new HashSet<Result>();
        }

        public CodeUpload(int studentId, int activityId) {
            Results = new HashSet<Result>();
            StudentId = studentId;
            ActivityId = activityId;
        }

        [Key]
        [Required]
        public int CodeUploadId { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [Required]
        public DateTime? UploadDate { get; set; }
        public string CodeUploadFileName { get; set; }
        public byte[] CodeUploadFile { get; set; }
        [StringLength(6000)]
        public string CodeUploadText { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int ActivityId { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<Result> Results { get; set; }

        public async Task AddCodeUpload() {
            await _repo.CreateAsync(this);
        }

        public async Task<CodeUpload> GetCodeUploadById(int studentId, int activityId) {
            return await _repo.FindByIdAsync(studentId, activityId);
        }

        public async Task UpdateCodeUpload(CodeUpload upload) {
            await _repo.UpdateAsync(upload);
        }

        public async Task AddSourceFileAsync(SourceFile sourceFile, int studentId) {
            ActivityId = sourceFile.ActivityId;
            StudentId = studentId;
            UploadDate = DateTime.Now;
            if (sourceFile.FileUpload.Length != 0) {
                CodeUploadText = await sourceFile.FileUpload.ReadAsStringAsync();
                using var ms = new MemoryStream();
                await sourceFile.FileUpload.CopyToAsync(ms);
                CodeUploadFile = ms.ToArray();
                CodeUploadFileName = sourceFile.FileUpload.FileName;
            }
            await AddCodeUpload();
        }

        public async Task UpdateSourceFileAsync(SourceFile sourceFile, int studentId, int codeUploadId) {
            CodeUploadId = codeUploadId;
            ActivityId = sourceFile.ActivityId;
            StudentId = studentId;
            UploadDate = DateTime.Now;
            if (sourceFile.FileUpload.Length != 0) {
                CodeUploadText = await sourceFile.FileUpload.ReadAsStringAsync();
                using var ms = new MemoryStream();
                await sourceFile.FileUpload.CopyToAsync(ms);
                CodeUploadFile = ms.ToArray();
                CodeUploadFileName = sourceFile.FileUpload.FileName;
            }
            await UpdateCodeUpload(this);
        }
    }
}
