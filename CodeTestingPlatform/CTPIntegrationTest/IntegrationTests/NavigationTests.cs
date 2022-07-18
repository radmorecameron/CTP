using CodeTestingPlatform;
using CodeTestingPlatform.Controllers;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CTPIntegrationTest.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPIntegrationTest.IntegrationTests {
    public class NavigationTests
        : IClassFixture<CTPWebApplicationFactory<Startup>> {
        private readonly CTPWebApplicationFactory<Startup> _factory;

        public NavigationTests(CTPWebApplicationFactory<Startup> factory) {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Login")]
        [InlineData("/Home")]
        [InlineData("/Home/About")]
        [InlineData("/Home/Help")]
        //[InlineData("/Contact")]
        public async Task NavigateAllowedPagesForUnauthorizedUser(string url) {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Activity")]
        [InlineData("/Activity/Create")]
        [InlineData("/Activity/Delete")]
        [InlineData("/Activity/Details")]
        [InlineData("/Activity/Edit")]
        [InlineData("/CodeUpload/CodeSubmission")]
        [InlineData("/Course")]
        [InlineData("/Course/Details")]
        [InlineData("/Course/StudentDetails")]
        [InlineData("/Parameter")]
        [InlineData("/Parameter/Edit")]
        [InlineData("/MethodSignature/Create")]
        [InlineData("/MethodSignature/Delete/1")]
        [InlineData("/MethodSignature/Details/1")]
        [InlineData("/Student")]
        [InlineData("/Teacher")]
        [InlineData("/TestCase")]
        [InlineData("/TestCase/Create")]
        [InlineData("/TestCase/Delete")]
        [InlineData("/TestCase/Details")]
        [InlineData("/TestCase/Edit")]
        public async Task NavigateDisallowedPagesForUnauthorizedUser(string url) {
            // Arrange
            HttpClient client = _factory.CreateClient(new WebApplicationFactoryClientOptions {
                AllowAutoRedirect = false
            });

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }

        // TODO: Add a test when a teacher signs in and logs out. Check if credentials are removed by navigating through the application when logged out.
    }
}
