using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models {
    public class SourceFile {
        [Required(ErrorMessage = "Please select a file.")]
        [MaxFileSize(15000)]
        [AllowedFileExtensions(new string[] { ".py", ".cs", ".js", ".java", ".php" })]
        public IFormFile FileUpload { get; set; }

        public int ActivityId { get; set; }
    }
}
