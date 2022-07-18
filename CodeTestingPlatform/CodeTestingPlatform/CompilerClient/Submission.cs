using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeTestingPlatform.CompilerClient {
    public class Submission {

        [JsonPropertyName("source_code")]
        public string SourceCode { get; set; }

        [JsonPropertyName("language_id")]
        public int LanguageId { get; set; }

        [JsonPropertyName("compiler_options")]
        public string CompilerOptions { get; set; }

        [JsonPropertyName("command_line_arguments")]
        public string CommandLineArguments { get; set; }

        [JsonPropertyName("stdin")]
        public string StandardInput { get; set; }

        [JsonPropertyName("expected_output")]
        public string ExpectedOutput { get; set; }

        [JsonPropertyName("additional_files")]
        public string AdditionalFiles { get; set; }

        [JsonPropertyName("stdout")]
        public string StandardOutput { get; set; }

        [JsonPropertyName("stderr")]
        public string StandardError { get; set; }

        [JsonPropertyName("compile_output")]
        public string CompilerOutput { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }
    }
}
