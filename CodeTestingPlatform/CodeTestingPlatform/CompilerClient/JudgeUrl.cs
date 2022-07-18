using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.CompilerClient {
    public class JudgeUrl {
        private readonly IConfiguration _config;

        public JudgeUrl() {
            _config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();
        }

        public string GetUrl() {
            return _config["Judge0Url"];
        }

        public string GetUrlBatch() {
            return _config["Judge0Url"] + "/batch/";
        }
    }
}
