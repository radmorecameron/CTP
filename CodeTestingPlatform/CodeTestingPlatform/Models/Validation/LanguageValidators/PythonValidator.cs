using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models.Validation.LanguageValidators {
    public class PythonValidator : ILanguageValidator {
        private static readonly List<string> pythonKeywords = new() { "and", "as", "assert", "break", "class", "continue", "def", "del", "elif", "else", "except", "False", "finally", "for", "from", "global", "if", "import", "in", "is", "lambda", "None", "nonlocal", "not", "or", "pass", "raise", "return", "True", "try", "while", "with", "yield" };
        public ValidatorResult IsValidMethodName(string signature) {
            // https://pep8.org/#function-names
            // no capital letters, might contain underscores
            ValidatorResult vr = ValidateRegex(signature);
            signature ??= "";
            if (vr.Result == ValidatorResult.ResultEnum.Success) {
                if (signature.ToLower() != signature) {
                    vr.Result = ValidatorResult.ResultEnum.Error;
                    vr.Message = $"{signature} does not follow pep8 standards";
                }
            }
            return vr;
        }

        public ValidatorResult IsValidParameter(SignatureParameter p) {
            // no capital letters (unless it ends with _co or _contra), might contain underscores. can't start with a number
            ValidatorResult vr = ValidateRegex(p.ParameterName);
            if (vr.Result == ValidatorResult.ResultEnum.Success) {
                if (p.ParameterName.ToLower() != p.ParameterName) {
                    if (p.ParameterName.EndsWith("_co") || p.ParameterName.EndsWith("_contra")) {
                        vr.Result = ValidatorResult.ResultEnum.Error;
                        vr.Message = $"{p.ParameterName} does not follow the pep8 standards";
                    }
                }
            }
            return vr;
        }
        private static ValidatorResult ValidateRegex(string name) {
            ValidatorResult vr = new() { Result = ValidatorResult.ResultEnum.Error };
            Regex startsWithNumber = new(@"^\d");
            Regex validCharacters = new(@"^[a-zA-Z_][a-zA-Z_0-9]*$");
            if (name == null) {
                vr.Result = ValidatorResult.ResultEnum.Success;
            } else if (pythonKeywords.Contains(name)) {
                vr.Message = $"{name} is a keyword in python";
            } else if (startsWithNumber.IsMatch(name)) {
                vr.Message = $"{name} starts with a number which is invalid in python";
            } else if (!validCharacters.IsMatch(name)) {
                vr.Message = $"{name} contains invalid characters in its name";
            } else {
                vr.Result = ValidatorResult.ResultEnum.Success;
            }
            return vr;
        }

        public string MethodFormat(MethodSignature ms) {
            string methodSignature = $"{ms.MethodName}(";
            SignatureParameter[] spArray = ms.SignatureParameters.ToArray();
            int paramCount = ms.SignatureParameters.Count;
            for (int i = 0; i < paramCount; i++) {
                SignatureParameter s = spArray[i];
                if (s.DefaultValue == null) {
                    //methodSignature += $"{s.ParameterName}: {s.DataType.DataType1}";
                    methodSignature += $"{s.ParameterName}: {((s.DataTypeId == (int)Data.Types.Other)? s.ObjectDataType : s.DataType.DataType1)}";
                } else {
                    //methodSignature += $"{s.ParameterName} = {s.DefaultValue} : {s.DataType.DataType1}";
                    methodSignature += $"{s.ParameterName} = {s.DefaultValue} : {((s.DataTypeId == (int)Data.Types.Other) ? s.ObjectDataType : s.DataType.DataType1)}";
                }
                if (i != paramCount - 1) {
                    methodSignature += ", ";
                }
            }
            methodSignature += ")";
            return methodSignature;
        }

        public string TestCaseFormat(TestCase tc) {
            List<string> strings = new() { "string", "String", "str" };
            string methodName = $"{tc.MethodSignature.MethodName}(";
            Parameter[] paramsArray = tc.Parameters.ToArray();
            for (var i = 0; i < tc.Parameters.Count; i++) {
                string dataType = tc.Parameters.ToArray()[i].SignatureParameter.DataType.DataType1;
                var value = strings.Contains(dataType, StringComparer.Ordinal) == true ? $"\"{paramsArray[i].Value}\"" : paramsArray[i].Value;
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
