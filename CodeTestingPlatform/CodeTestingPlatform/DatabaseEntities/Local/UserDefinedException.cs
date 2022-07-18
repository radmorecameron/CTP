using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class UserDefinedException {

        [Key]
        [Required]
        public int UserDefinedExceptionId { get; set; }

        [StringLength(40)]
        [Required]
        [Display(Name = "User-Defined Exception")]
        public string UserDefinedExceptionName { get; set; }

        [Required]
        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }

        public virtual ICollection<SignatureUserDefinedException> SignatureUserDefinedExceptions { get; set; }
    }
}
