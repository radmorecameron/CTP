using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.ViewModels {
    public class StudentResultVM {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? TotalTests { get; set; }
        public int? PassedTests { get; set; }
        public DateTime? UploadDate { get; set; }
        public string PassedTestsString {
            get {
                if (PassedTests != null) {
                    return PassedTests.ToString();
                } else {
                    return "N/A";
                }
            }
        }

        public string TotalTestsString {
            get {
                if (TotalTests != null) {
                    return TotalTests.ToString();
                } else {
                    return "N/A";
                }
            }
        }

        public string FailedTestsString {
            get {
                if (TotalTests != null && PassedTests != null) {
                    return (TotalTests - PassedTests).ToString();
                } else {
                    return "N/A";
                }
            }
        }

        public string PassTotalPercentageString {
            get {
                if (TotalTests != null && PassedTests != null) {
                    return $"{(float)(Math.Round((decimal)(((float)PassedTests) / TotalTests), 2)*100)}%";
                } else {
                    return "N/A";
                }
            }
        }
        public string UploadDateString {
            get {
                if (UploadDate != null) {
                    return UploadDate.Value.ToString("yyyy\\/MM\\/dd");
                } else {
                    return "N/A";
                }
            }
        }
    }
}
