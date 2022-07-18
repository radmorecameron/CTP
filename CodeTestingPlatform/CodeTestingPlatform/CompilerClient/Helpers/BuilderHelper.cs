using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestingPlatform.CompilerClient.Helpers {
    public class BuilderHelper {
        public static string BuildSourceFile(string filename, MethodSignature methodSignature, List<SignatureParameter> parameters, TestCase testCase) {
            return $@"
from {filename} import {methodSignature.MethodName}

def main():
{BuildInputVariables(parameters, testCase)}
    print({BuildMethodSignature(methodSignature, parameters)})

if __name__ == '__main__':
    main()
";
        }

//        public static string BuildSyntaxCheckFile(string sourceFile, List<MethodSignature> methodSignatures) {
//            return $@"
//{sourceFile}

//def main():
//{BuildInputVariables(parameters, testCase)}
//    print({BuildMethodSignature(methodSignature, parameters)})

//if __name__ == '__main__':
//    main()
//";
//        }

//        private static string BuildMethodCall(List<MethodSignature> methodSignatures) {
//            StringBuilder builder = new();
//        }
        private static string BuildMethodSignature(MethodSignature methodSignature, List<SignatureParameter> parameters) {
            return $"{methodSignature.MethodName}({BuildParameters(parameters)})";
        }

        private static string BuildParameters(List<SignatureParameter> parameters) {
            List<string> parameterNames = parameters.OrderBy(x => x.ParameterPosition).ToList().ConvertAll(x => x.ParameterName);
            string names = string.Join(", ", parameterNames);
            return names;
        }

        private static string BuildInputVariables(List<SignatureParameter> parameters, TestCase testCase) {
            StringBuilder builder = new();

            int index = 0;
            foreach(Parameter testCaseParameter in testCase.Parameters) {
                SignatureParameter currentParameter = parameters[index];
                string parameterName = currentParameter.ParameterName;
                string datyType = currentParameter.DataType.DataType1;
                string values = testCaseParameter.Value;
                builder.Append($"    {BuildInputVariable(parameterName, datyType, values)}\n");
                index++;
            }
  
            return builder.ToString();
        }

        private static string BuildInputVariable(string parameterName, string parameterType, string values) {
            string inputVariable = string.Empty;

            if (parameterType == "str") {
                inputVariable = $"{parameterName} = str(input())";
            } else if (parameterType == "int") {
                inputVariable = $"{parameterName} = int(input())";
            } else if (parameterType == "float") {
                inputVariable = $"{parameterName} = float(input())";
            } else if (parameterType == "bool") {
                inputVariable = $"{parameterName} = bool(input())";
            } else if (parameterType == "list") {
                inputVariable = $"{parameterName} = {values}";
            } else if (parameterType == "set") {
                inputVariable = $"{parameterName} = {values}";
            } else if (parameterType == "tuple") {
                inputVariable = $"{parameterName} = {values}";
            } else if (parameterType == "complex") {
                inputVariable = $"{parameterName} = {values}";
            }

            return inputVariable;
        }
    }
}
