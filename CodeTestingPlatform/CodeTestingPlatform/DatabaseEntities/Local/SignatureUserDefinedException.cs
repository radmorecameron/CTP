using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public class SignatureUserDefinedException {
        public SignatureUserDefinedException() {

        }

        public SignatureUserDefinedException(int signatureId, int userDefinedExceptionId) : this() {
            SignatureId = signatureId;
            UserDefinedExceptionId = userDefinedExceptionId;
        }

        [Key]
        [Required]
        public int SignatureUserDefinedExceptionId { get; set; }

        [Required]
        public int SignatureId { get; set; }

        [Required]
        public int UserDefinedExceptionId { get; set; }

        public virtual MethodSignature MethodSignature{ get; set; }

        public virtual UserDefinedException UserDefinedException { get; set; }
    }
}
