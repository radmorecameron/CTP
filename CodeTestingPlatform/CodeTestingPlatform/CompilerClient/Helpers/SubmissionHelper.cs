using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.CompilerClient.Helpers {
    public static class SubmissionHelper {

        public static string GetCommandListArgumentsFromList(string list) {
            int openArray;
            int closeArray;
            openArray = list.IndexOf('[');
            closeArray = list.IndexOf(']');

            string command_line_list = list.Substring(openArray + 1, closeArray - 1);
            
            return command_line_list.Replace(',', ' ');
        }

        public static Submission GetSubmission(string sourceCode, int languageId) {
            return new Submission {
                SourceCode = sourceCode,
                LanguageId = languageId,
            };
        }

        public static Submission GetSubmission(string sourceCode, int languageId, string additionalFiles, string inputValues, string expectedResult) {
            return new Submission {
                SourceCode = sourceCode,
                LanguageId = languageId,
                AdditionalFiles = additionalFiles,
                StandardInput = inputValues,
                ExpectedOutput = expectedResult,
            };
        }
    }
}
