using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CTPIntegrationTest.Helpers {
    public static class TokenParser {
        public static string GetVerificationToken(HttpResponseMessage response) {
            string verificationToken = response.Content.ReadAsStringAsync().Result;
            if (verificationToken != null && verificationToken.Length > 0) {
                verificationToken = verificationToken[verificationToken.IndexOf("__RequestVerificationToken")..];
                verificationToken = verificationToken[(verificationToken.IndexOf("value=\"") + 7)..];
                verificationToken = verificationToken.Substring(0, verificationToken.IndexOf("\""));
            }
            return verificationToken;
        }
    }
}
