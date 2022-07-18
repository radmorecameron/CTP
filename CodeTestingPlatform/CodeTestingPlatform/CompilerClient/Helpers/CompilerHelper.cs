using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CodeTestingPlatform.CompilerClient.Helpers {
    public static class CompilerHelper {
        public static StringContent SerializeSubmission(Submission codeSubmission) {
            string json = JsonSerializer.Serialize(codeSubmission);
            return ConvertToStringContent(json);
        }

        public static StringContent SerializeSubmission(string sourceCode, int languageId) {
            Submission code = new() {
                LanguageId = languageId,
                SourceCode = sourceCode,
            };

            string json = JsonSerializer.Serialize(code);
            return ConvertToStringContent(json);
        }

        public static StringContent SerializeSubmissionBatch(SubmissionBatch codeBatch) {
            string json = JsonSerializer.Serialize(codeBatch);
            return ConvertToStringContent(json);
        }

        public static StringContent SerializeToken(SubmissionToken submissionToken) {
            string json = JsonSerializer.Serialize(submissionToken);
            return ConvertToStringContent(json);
        }

        public static StringContent ConvertToStringContent(string content) {
            StringContent token = new(content, Encoding.UTF8, "application/json");
            return token;
        }
    }
}
