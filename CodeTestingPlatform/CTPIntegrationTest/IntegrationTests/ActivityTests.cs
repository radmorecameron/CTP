using CodeTestingPlatform;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CTPIntegrationTest.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPIntegrationTest.IntegrationTests {
    public class ActivityTests
        : IClassFixture<CTPWebApplicationFactory<Startup>> {
        private readonly CTPWebApplicationFactory<Startup> _factory;

        public ActivityTests(CTPWebApplicationFactory<Startup> factory) {
            _factory = factory;
        }

        [Fact]
        public async Task CheckTeacherActivityList() {
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
            HttpResponseMessage courseResponse = await client.GetAsync("Course/Details/20");
            string indexContent = await loginResponse.Content.ReadAsStringAsync();
            string courseContent = await courseResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, loginResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, courseResponse.StatusCode);
            Assert.Contains("Development Projects I", indexContent);
            Assert.Contains("This course does not have any activities.", courseContent);
        }

        [Fact]
        public async Task CheckTeacherActivityDetails() {
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
            HttpResponseMessage activityResponse = await client.GetAsync("Activity/Details/66");
            string indexContent = await loginResponse.Content.ReadAsStringAsync();
            string activityContent = await activityResponse.Content.ReadAsStringAsync();

            // Assert

            // check response return 200 status
            Assert.Equal(HttpStatusCode.OK, loginResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, activityResponse.StatusCode);

            // check if successfully login goes to courses page
            Assert.Contains("Courses", indexContent);
            Assert.Contains("Development Project II", indexContent);
            Assert.Contains("Course/Details/20", indexContent);

            // check the activity content
            Assert.Contains("Development Project II", activityContent);
            Assert.Contains("Labs", activityContent);
            Assert.Contains("C#", activityContent);
            Assert.Contains("Big Test", activityContent);
            Assert.DoesNotContain("DooDoo", activityContent);
        }
    }
}
