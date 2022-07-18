using CodeTestingPlatform.CompilerClient.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CodeTestingPlatform.CompilerClient {
    public class Compiler : ICompiler {
        private readonly HttpClient _client;
        private readonly SubmissionBatch _batch;
        private readonly JudgeUrl judgeUrl;
        private List<SubmissionToken> _tokens;
        private Submission _submission;
        private SubmissionToken _token;
        

        public Compiler() {
            _client = new();
            _tokens = new();
            _batch = new();
            judgeUrl = new();
        }

        /// <summary>
        /// The <c>GetResultsAsync</c> method is used to receive a single submission response from the Judge0 compiler. 
        /// </summary>///
        public async Task<Submission> GetResultsAsync() {
            Thread.Sleep(2000);
            HttpResponseMessage response = await _client.GetAsync($"{judgeUrl.GetUrl()}/{_token.Token}");
            Stream submissionResponse = await response.Content.ReadAsStreamAsync();
            Submission submissionResult = await JsonSerializer.DeserializeAsync<Submission>(submissionResponse);

            return submissionResult;
        }

        /// <summary>
        /// The <c>GetResultsBatchAsync</c> method is used to receive a multiple submission response from the Judge0 compiler. 
        /// </summary>///
        public async Task<List<Submission>> GetResultsBatchAsync() {
            Thread.Sleep(4000);
            List<string> tokens = _tokens.ToList().ConvertAll(x => x.Token);
            string tokensUrl = string.Join(",", tokens);
            HttpResponseMessage response = await _client.GetAsync($"{judgeUrl.GetUrlBatch()}/?tokens={tokensUrl}");
            Stream submissionResponse = await response.Content.ReadAsStreamAsync();
            SubmissionBatch submissionBatchResult = await JsonSerializer.DeserializeAsync<SubmissionBatch>(submissionResponse);
            return submissionBatchResult.Submissions.ToList();
        }

        /// <summary>
        /// The <c>SubmitCodeAsync</c> method is used to send a single submission request to the Judge0 compiler. 
        /// </summary>///
        public async Task<SubmissionToken> SubmitCodeAsync(string sourceCode, int languageId) {
            _submission = GetSubmission(sourceCode, languageId);
            StringContent content = CompilerHelper.SerializeSubmission(_submission);
            HttpResponseMessage response = await _client.PostAsync(judgeUrl.GetUrl(), content);
            SubmissionToken token = await DeserializeResponseAsync(response);
            _token = token;
            return _token;
        }

        /// <summary>
        /// The <c>SubmitCodeAsync</c> method is used to send a single submission request to the Judge0 compiler. 
        /// </summary>///
        public async Task<SubmissionToken> SubmitCodeAsync(Submission submission) {
            _submission = submission;
            StringContent content = CompilerHelper.SerializeSubmission(_submission);
            HttpResponseMessage response = await _client.PostAsync(judgeUrl.GetUrl(), content);
            SubmissionToken token = await DeserializeResponseAsync(response);
            _token = token;
            return _token;
        }

        /// <summary>
        /// The <c>SubmitCodeBatchAsync</c> method is used to send multiple submissions in a single request to the Judge0 compiler. 
        /// </summary>///
        public async Task<List<SubmissionToken>> SubmitCodeBatchAsync(List<Submission> submissions) {
            _batch.Submissions = submissions;
            StringContent content = CompilerHelper.SerializeSubmissionBatch(_batch);
            HttpResponseMessage response = await _client.PostAsync(judgeUrl.GetUrlBatch(), content);
            List<SubmissionToken> tokens = await DeserializeResponseBatchAsync(response);
            _tokens = tokens;
            return _tokens;
        }

        private static async Task<SubmissionToken> DeserializeResponseAsync(HttpResponseMessage response) {
            Stream stream = await response.Content.ReadAsStreamAsync();
            SubmissionToken token = await JsonSerializer.DeserializeAsync<SubmissionToken>(stream);
            return token;
        }

        private static async Task<List<SubmissionToken>> DeserializeResponseBatchAsync(HttpResponseMessage response) {
            Stream stream = await response.Content.ReadAsStreamAsync();
            List<SubmissionToken> batchTokens = await JsonSerializer.DeserializeAsync<List<SubmissionToken>>(stream);
            return batchTokens;
        }

        private static Submission GetSubmission(string sourceCode, int languageId) {
            return new Submission {
                SourceCode = sourceCode,
                LanguageId = languageId,
            };
        }
    }
}
