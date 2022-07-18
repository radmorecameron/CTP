using CodeTestingPlatform.DatabaseEntities.Local;
using CTPTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Models {
    public class LanguageEntityTest {
        public readonly TestSetup ts;
        public LanguageEntityTest() {
            ts = new();
        }
        /*
        [Fact]
        public async Task TestLanguage_GetLanguages() {
            using var context = ts.CreateContext();
            Language language = new(context);
            List<Language> langs = await language.GetLanguages();
            Assert.Equal(langs.OrderBy(x => x.LanguageName).ToList(), langs.ToList());
        }
        */
    }
}
