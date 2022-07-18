using System;
using System.Diagnostics.CodeAnalysis;

namespace CodeTestingPlatform.Models {
    public interface IDateTimeProvider {
        DateTime Now { get; }
        string ToShortDateString();
    }

    [ExcludeFromCodeCoverage]
    public class DateTimeProvider : IDateTimeProvider {
        public DateTime Now => DateTime.Now;
        public string ToShortDateString() => Now.ToShortDateString();

    }
}
