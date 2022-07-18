using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models {
    public class StudentResult {
        public int MethodSignatureId { get; set; }
        public string MethodName { get; set; }
        public int Passed { get; set; }
        public int Failed { get; set; }
        public int Total { get { return Passed + Failed; } }
    }
}
