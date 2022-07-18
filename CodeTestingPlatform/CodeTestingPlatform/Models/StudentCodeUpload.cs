using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models {
    public class StudentCodeUpload {
        public IEnumerable TestIds { get; set; }
        public Activity Activity { get; set; }
        public SourceFile SourceFile { get; set; }

        public List<Result> Results { get; set; }
        public List<StudentResult> StudentResults { get; set; } = new List<StudentResult>();

        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string Error { get; set; }
        public string FileName { get; set; }
        public DateTime FileUploadedDate { get; set; }
    }
}
