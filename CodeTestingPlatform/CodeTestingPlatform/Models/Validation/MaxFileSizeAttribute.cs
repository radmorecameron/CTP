using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models.Validation {
    public class MaxFileSizeAttribute : ValidationAttribute {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize) {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if (value is IFormFile file) {
                if (file.Length > _maxFileSize) {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }

        private string GetErrorMessage() {
            return $"Maximum allowed file size is {_maxFileSize} bytes.";
        }
    }
}
