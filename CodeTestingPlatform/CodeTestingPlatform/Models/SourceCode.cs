using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models {
    public class SourceCode {
        [Required(ErrorMessage = "No code entered.")]
        [StringLength(6000, ErrorMessage = "Code exceeds maximum of 6000 characters.")]
        public string CodeText { get; set; }
    }
}
