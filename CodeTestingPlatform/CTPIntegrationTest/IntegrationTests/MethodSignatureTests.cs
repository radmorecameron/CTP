using CodeTestingPlatform;
using CTPIntegrationTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPIntegrationTest.IntegrationTests {
    public class MethodSignatureTests
        : IClassFixture<CTPWebApplicationFactory<Startup>> {
        private readonly CTPWebApplicationFactory<Startup> _factory;

        public MethodSignatureTests(CTPWebApplicationFactory<Startup> factory) {
            _factory = factory;
        }

        [Fact]
        public async Task CheckTeacherMethodSignatureDetails() {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage loginResult = await client.GetAsync("/login");
            string verificationToken = TokenParser.GetVerificationToken(loginResult);
            HttpResponseMessage loginResponse = await client.PostAsync("/login", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Username", "userco"),
                new KeyValuePair<string, string>("Password", "cs@123test!"),
                new KeyValuePair<string, string>("__RequestVerificationToken", verificationToken),
            }));
            HttpResponseMessage methodSignatureResponse = await client.GetAsync("MethodSignature/Details/1");
            string indexContent = await loginResponse.Content.ReadAsStringAsync();
            string methodSignatureContent = await methodSignatureResponse.Content.ReadAsStringAsync();

            // Assert

            // check if responses return 200
            Assert.Equal(HttpStatusCode.OK, loginResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, methodSignatureResponse.StatusCode);

            // check if login goes to courses page
            Assert.Contains("Courses", indexContent);

            // check the content for the method signature page
            Assert.Contains("get_evens", methodSignatureContent);
            Assert.Contains("Programming III", methodSignatureContent);
            Assert.Contains("Lab 2 (Python v)", methodSignatureContent);
            Assert.Contains("Get all even numbers in a list", methodSignatureContent);
            Assert.DoesNotContain("DooDoo", methodSignatureContent);
        }
    }
}
