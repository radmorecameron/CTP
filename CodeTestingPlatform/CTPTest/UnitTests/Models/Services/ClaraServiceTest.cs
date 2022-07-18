using CodeTestingPlatform.DatabaseEntities.Clara;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Repositories;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services;
using CodeTestingPlatform.Services.Interfaces;
using CTPTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CTPTest.UnitTests.Models.Services {
    public class ClaraServiceTest {
        private readonly TestSetup ts;
        public ClaraServiceTest() {
            ts = new();
        }
        public IClaraService CreateClaraServiceTest() {
            CTP_TESTContext ctx = ts.CreateContext();
            IClaraRepository icr = new ClaraRepository(ctx);
            IClaraService ics = new ClaraService(icr);
            return ics;
        }
        [Fact]
        public async Task GetClaraCSStudents() {
            IClaraService cService = CreateClaraServiceTest();
            List<ClaraCSStudent> studList = await cService.GetClaraCSStudents();
            Assert.NotEmpty(studList);
        }
 
    }
}
