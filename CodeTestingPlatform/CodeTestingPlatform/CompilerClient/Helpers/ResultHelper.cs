using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.CompilerClient.Helpers {
    public static class ResultHelper {
        public static bool PassOrFailResult(Submission submission) {
            if(submission.StandardError == null 
                && submission.StandardOutput != null 
                && submission.Status.StatusId == 3 
                && submission.Status.Description == "Accepted") {
                return true;
            }

            return false;
        }
    }
}
