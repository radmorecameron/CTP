using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models {
    public class ActivityResults {
        public Activity Activity { get; set; }
        [Display(Name = "Total Students: ")]
        public int TotalStudents { get; set; } = 0;
        [Display(Name = "Students That Uploaded Code: ")]
        public int StudentsThatUploaded { get; set; } = 0;
        [Display(Name = "Students That Ran Code: ")]
        public int StudentsThatRanCode { get; set; } = 0;
        [Display(Name = "Total TestCases Ran: ")]
        public int TestsRan { get; set; } = 0;
        [Display(Name = "Total TestCases Passed: ")]
        public int TestsPassed { get; set; } = 0;
        public List<StudentResultVM> StudentResults { get; set; }
        public ActivityResults(List<Ctpuser> users, Activity activity) {
            Activity = activity;
            StudentResults = new();
            foreach (Ctpuser user in users) {
                CodeUpload cu = user.Student.CodeUploads
                    .FirstOrDefault(cu => cu.ActivityId == activity.ActivityId);
                List<Result> results = new();
                int? totalResults = null;
                int? passedResults = null;
                TotalStudents++;
                if (cu != null) {
                    StudentsThatUploaded += 1;
                    results = cu.Results.ToList();
                    totalResults = results.Count;
                    passedResults = results.Where(t => t.PassFail == true).Count();
                    TestsRan += totalResults.Value;
                    TestsPassed += passedResults.Value;
                }

                if (totalResults == 0) {
                    totalResults = null;
                    passedResults = null;
                }

                StudentResultVM curStud = new() {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    TotalTests = totalResults,
                    PassedTests = passedResults,
                    UploadDate = cu?.UploadDate
                };
                StudentResults.Add(curStud);
            }
        }
    }
}
