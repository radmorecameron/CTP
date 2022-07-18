using CodeTestingPlatform.DatabaseEntities.Local;
using CTPIntegrationTest.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPIntegrationTest.IntegrationTests {
    public class LoginStudentTests
        : IClassFixture<CTPWebApplicationFactory<CodeTestingPlatform.Startup>> {
        private readonly CTPWebApplicationFactory<CodeTestingPlatform.Startup> _factory;

        public LoginStudentTests(CTPWebApplicationFactory<CodeTestingPlatform.Startup> factory) {
            _factory = factory;
        }

        [Fact]
        public async Task TestStudentLoginInvalidPassword() {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage loginResult = await client.GetAsync("/login");
            string verificationToken = TokenParser.GetVerificationToken(loginResult);
            HttpResponseMessage incorrectResponse = await client.PostAsync("/login", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Username", "1111111"),
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
        public async Task TestStudentLoginInvalidUsername() {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage loginResult = await client.GetAsync("/login");
            string verificationToken = TokenParser.GetVerificationToken(loginResult);
            HttpResponseMessage incorrectResponse = await client.PostAsync("/login", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Username", "fakeuser"),
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
        public async Task TestStudentLoginCheckNonComputerScienceStudent() {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage loginResult = await client.GetAsync("/login");
            string verificationToken = TokenParser.GetVerificationToken(loginResult);
            HttpResponseMessage notCompSciStudentResponse = await client.PostAsync("/login", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Username", "1111111"),
                new KeyValuePair<string, string>("Password", "cs@123test!"),
                new KeyValuePair<string, string>("__RequestVerificationToken", verificationToken),
            }));
            string notCompSciStudentContent = await notCompSciStudentResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, loginResult.StatusCode);
            Assert.Contains("Only computer science students are allowed to access this application", notCompSciStudentContent);
        }

        [Fact]
        public async Task TestStudentLoginSuccessful() {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage loginResult = await client.GetAsync("/login");
            string verificationToken = TokenParser.GetVerificationToken(loginResult);
            HttpResponseMessage validUserResponse = await client.PostAsync("/login", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Username", "3333333"),
                new KeyValuePair<string, string>("Password", "cs@123test!"),
                new KeyValuePair<string, string>("__RequestVerificationToken", verificationToken),
            }));

            string validUserContent = await validUserResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, loginResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, validUserResponse.StatusCode);
            Assert.Contains("Student Page", validUserContent);
        }
    }
}
