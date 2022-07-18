using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models.Extensions {

    public static class IFormFileExtension {
        public static async Task<string> ReadAsStringAsync(this IFormFile file) {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream())) {
                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadLineAsync());
            }
            return result.ToString();
        }
    }
}
