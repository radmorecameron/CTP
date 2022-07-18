using CodeTestingPlatform.CompilerClient;
using CodeTestingPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Controllers {
    public class TestSubmissionController : Controller {
        public async Task<IActionResult> Index() {
            Compiler compiler = new();
            int language = 71;
            string sourceCode = @"def play():
    print(f'Hello world!')
play()";

            //compiler.AddSubmission(sourceCode, language);
            //await compiler.SubmitCodeAsync();
            //Submission submission = await compiler.GetResultsAsync();


            return View(new Submission());
        }
    }
}
