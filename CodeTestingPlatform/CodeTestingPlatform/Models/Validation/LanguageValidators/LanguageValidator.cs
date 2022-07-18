using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models.Validation.LanguageValidators {
    public class LanguageValidator {
        public static ILanguageValidator GetLanguageValidator(int langId) {
            if (langId == 71) {
                return new PythonValidator();
            } else {
                return new CSharpValidator();
            }
        }
    }
}
