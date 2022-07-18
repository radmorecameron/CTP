using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class Result {
        [Key]
        [Required]
        public int ResultId { get; set; }
        public bool? PassFail { get; set; }
        [StringLength(2000)]
        public string ErrorMessage { get; set; }
        [StringLength(255)]
        public string ActualValue { get; set; }
        //[Required]
        //public int ActivityId { get; set; }
        public int? CodeUploadId { get; set; }
        [Required]
        public int TestCaseId { get; set; }

        public virtual CodeUpload CodeUpload { get; set; }
        public virtual TestCase TestCase { get; set; }
    }
}
