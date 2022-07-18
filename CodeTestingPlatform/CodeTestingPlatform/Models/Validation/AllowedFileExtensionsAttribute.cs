using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models.Validation {
    public class AllowedFileExtensionsAttribute : ValidationAttribute {
        private readonly string[] _extensions;
        private string extension;

        public AllowedFileExtensionsAttribute(string[] allowedExtensions) {
            _extensions = allowedExtensions;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if (value is IFormFile file) {
                extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension)) {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }

        private string GetErrorMessage() {
            return $"File extension {extension} is not allowed.";
        }
    }
}
