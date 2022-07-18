using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models.Validation.LanguageValidators {
    public class CSharpValidator : ILanguageValidator {
        public ValidatorResult IsValidMethodName(string signature) {
            return new ValidatorResult { Result = ValidatorResult.ResultEnum.Success };
        }

        public ValidatorResult IsValidParameter(SignatureParameter p) {
            return new ValidatorResult { Result = ValidatorResult.ResultEnum.Success };
        }

        public string MethodFormat(MethodSignature ms) {
            string methodSignature = $"{ms.MethodName}(";
            SignatureParameter[] spArray = ms.SignatureParameters.ToArray();
            int paramCount = ms.SignatureParameters.Count;
            for (int i=0; i < paramCount; i++) {
                SignatureParameter s = spArray[i];
                if (i == paramCount - 1) {
                    methodSignature += $"{s.DataType.DataType1} {s.ParameterName}";
                } else {
                    methodSignature += $"{s.DataType.DataType1} {s.ParameterName}, "; 
                }
            }
            methodSignature += ")";
            return methodSignature;
        }

        public string TestCaseFormat(TestCase tc) {
            List<string> strings = new() { "string", "String", "str" };
            var methodName = $"{tc.MethodSignature.MethodName}(";
            for (var i = 0; i < tc.Parameters.Count; i++) {
                string dataType = tc.Parameters.ToArray()[i].SignatureParameter.DataType.DataType1;
                var value = strings.Contains(dataType, StringComparer.Ordinal) == true ? $"\"{tc.Parameters.ToArray()[i].Value}\"" : tc.Parameters.ToArray()[i].Value;
                if (i == tc.Parameters.Count - 1) {
                    methodName += $"{value}";
                } else {
                    methodName += $"{value}, ";
                }
            }
            methodName += ")";
            return methodName;
        }
    }
}
