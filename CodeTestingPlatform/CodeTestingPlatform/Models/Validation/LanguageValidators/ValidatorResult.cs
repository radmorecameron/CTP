using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models.Validation.LanguageValidators {
    public class ValidatorResult {
        public enum ResultEnum { Success=0, Error=1}
        public ResultEnum Result { get; set; }
        public string Message { get; set; }
    }
}
