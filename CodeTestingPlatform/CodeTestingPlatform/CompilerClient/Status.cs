using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CodeTestingPlatform.CompilerClient {
    public class Status {

        [JsonPropertyName("id")]
        public int StatusId { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

    }
}
