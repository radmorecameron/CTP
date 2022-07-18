using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models
{
    [ExcludeFromCodeCoverageAttribute]
    public class ResponseStatus
    {
        public int Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
