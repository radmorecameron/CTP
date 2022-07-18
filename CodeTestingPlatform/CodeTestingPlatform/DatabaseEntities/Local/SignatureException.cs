using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class SignatureException {
        public SignatureException() {
        }

        public SignatureException(int signatureId, int exceptionId) : this() {
            SignatureId = signatureId;
            ExceptionId = exceptionId;
        }

        [Key]
        [Required]
        public int SignatureExceptionId { get; set; }
        [Required]
        public int SignatureId { get; set; }
        [Required]
        public int ExceptionId { get; set; }

        public virtual MethodSignature MethodSignature { get; set; }
        public virtual Exception Exception { get; set; }
    }
}
