using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public class Exception {
        public Exception() {
        }

        [Key]
        [Required]
        public int ExceptionId { get; set; }
        [StringLength(40)]
        [Required]
        [Display(Name = "Exception Name")]
        public string ExceptionName { get; set; }
        [Required]
        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }
        public virtual ICollection<SignatureException> SignatureExceptions { get; set; }
        public virtual ICollection<TestCaseException> TestCaseExceptions { get; set; }
    }
}
