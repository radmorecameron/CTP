using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class TestCaseException {
        public TestCaseException() {
        }

        public TestCaseException(int testCaseId, int exceptionId) : this() {
            TestCaseId = testCaseId;
            ExceptionId = exceptionId;
        }

        [Key]
        [Required]
        public int TestCaseExceptionId { get; set; }
        [Required]
        public int TestCaseId { get; set; }
        [Required]
        public int ExceptionId { get; set; }

        public virtual TestCase TestCase { get; set; }
        public virtual Exception Exception { get; set; }
    }
}
