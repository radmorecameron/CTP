using CodeTestingPlatform.Models.Validation.LanguageValidators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class TestCase {
        public TestCase() {
            Parameters = new HashSet<Parameter>();
            Results = new HashSet<Result>();
        }
        [Key]
        public int TestCaseId { get; set; }
        [ForeignKey("SignatureId")]
        [Required]
        public int MethodSignatureId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Test Case Name")]
        public string TestCaseName { get; set; }
        [Display(Name = "Expected Value")]
        public string ExpectedValue { get; set; }
        [Display(Name = "Validate Test Case:")]
        public bool ValidateTestCase { get; set; } = true;

        public virtual MethodSignature MethodSignature { get; set; }
        public virtual TestCaseException TestCaseException { get; set; }
        public virtual ICollection<Parameter> Parameters { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}
