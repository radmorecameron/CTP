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
    public class LoginTeacherTests
        : IClassFixture<CTPWebApplicationFactory<CodeTestingPlatform.Startup>> {

        private readonly CTPWebApplicationFactory<CodeTestingPlatform.Startup> _factory;

        public LoginTeacherTests(CTPWebApplicationFactory<Startup> factory) {
            _factory = factory;
        }

        [Fact]
        public async Task TestTeacherLoginInvalidPassword() {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage loginResult = await client.GetAsync("/login");
            string verificationToken = TokenParser.GetVerificationToken(loginResult);
            HttpResponseMessage incorrectResponse = await client.PostAsync("/login", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Username", "userte"),
                new KeyValuePair<string, string>("Password", "fakepassword"),
                new KeyValuePair<string, string>("__RequestVerificationToken", verificationToken),
            }));
            string incorrectContent = await incorrectResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, loginResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, incorrectResponse.StatusCode);
            Assert.Contains("The username or password you have entered is incorrect.", incorrectContent);
        }

        [Fact]
        public async Task TestTeacherLoginInvalidUsername() {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage loginResult = await client.GetAsync("/login");
            string verificationToken = TokenParser.GetVerificationToken(loginResult);
            HttpResponseMessage incorrectResponse = await client.PostAsync("/login", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Username", "fakeuser"),
                new KeyValuePair<string, string>("Password", "cs@123test!"),
                new KeyValuePair<string, string>("__RequestVerificationToken", verificationToken),
            }));

            string incorrectContent = await incorrectResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, loginResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, incorrectResponse.StatusCode);
            Assert.Contains("The username or password you have entered is incorrect.", incorrectContent);
        }

        [Fact]
        public async Task TestTeacherLoginSuccessful() {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage loginResult = await client.GetAsync("/login");
            string verificationToken = TokenParser.GetVerificationToken(loginResult);
            HttpResponseMessage validUserResponse = await client.PostAsync("/login", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Username", "userco"),
                new KeyValuePair<string, string>("Password", "cs@123test!"),
                new KeyValuePair<string, string>("__RequestVerificationToken", verificationToken),
            }));

            string validUserContent = await validUserResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, loginResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, validUserResponse.StatusCode);
            Assert.Contains("Home", validUserContent);
        }
    }
}
