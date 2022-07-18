using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CodeTestingPlatform.CompilerClient {
    public class SubmissionToken {

        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
