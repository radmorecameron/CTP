using CodeTestingPlatform;
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
    public class CourseTeacherTests
        : IClassFixture<CTPWebApplicationFactory<Startup>> {
        private readonly CTPWebApplicationFactory<Startup> _factory;

        public CourseTeacherTests(CTPWebApplicationFactory<Startup> factory) {
            _factory = factory;
        }

        [Fact]
        public async Task CheckTeacherCourses() {

            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage loginRequest = await client.GetAsync("/login");
            string verificationToken = TokenParser.GetVerificationToken(loginRequest);
            HttpResponseMessage loginResponse = await client.PostAsync("/login", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Username", "userco"),
                new KeyValuePair<string, string>("Password", "cs@123test!"),
                new KeyValuePair<string, string>("__RequestVerificationToken", verificationToken),
            }));

            string indexContent = await loginResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, loginRequest.StatusCode);
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
            Assert.Contains(">Development Projects I", indexContent);
            Assert.Contains("/Course/Details/30", indexContent);
        }
    }
}
