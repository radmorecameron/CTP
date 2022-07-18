using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CodeTestingPlatform.CompilerClient {
    public class SubmissionBatch {
        public SubmissionBatch()
        {
            Submissions = new List<Submission>();
        }

        [JsonPropertyName("submissions")]
        public IList<Submission> Submissions { get; set; }
    }
}
