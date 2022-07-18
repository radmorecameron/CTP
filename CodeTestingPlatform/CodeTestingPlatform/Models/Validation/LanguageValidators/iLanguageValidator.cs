using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models.Validation.LanguageValidators {
    public interface ILanguageValidator {
        /*
         * An interface to help validate and format
         * code for different languages
         */
        public ValidatorResult IsValidMethodName(string signature);
        public ValidatorResult IsValidParameter(SignatureParameter p);
        public string MethodFormat(MethodSignature ms);
        public string TestCaseFormat(TestCase tc);
    }
}
