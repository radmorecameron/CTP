using System;
using System.Diagnostics.CodeAnalysis;

namespace CodeTestingPlatform.Models {
    [ExcludeFromCodeCoverageAttribute]
    public class ErrorViewModel {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
