using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.CompilerClient {
    public interface ICompiler {

        Task<SubmissionToken> SubmitCodeAsync(string sourceCode, int languageId);

        Task<SubmissionToken> SubmitCodeAsync(Submission submission);

        Task<List<SubmissionToken>> SubmitCodeBatchAsync(List<Submission> submissions);

        Task<Submission> GetResultsAsync();

        Task<List<Submission>> GetResultsBatchAsync();
    }
}
