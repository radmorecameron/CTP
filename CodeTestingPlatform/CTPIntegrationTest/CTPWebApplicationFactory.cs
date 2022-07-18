using CodeTestingPlatform.DatabaseEntities.Local;
using CTPIntegrationTest.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTPIntegrationTest {
    public class CTPWebApplicationFactory<TStartup>
       : WebApplicationFactory<TStartup> where TStartup : class {
        private readonly SqliteConnection _connection;

        public CTPWebApplicationFactory() {

            // Gets an SQLite In-Memory connection and opens a connection
            _connection = DBHelper.GetSqliteInMemoryConnection();
            _connection.Open();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder) {
            builder.ConfigureServices(async services => {

                // Override the CTP startup class and remove CTP DbContext to use SQLite In-Memory
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CTP_TESTContext>));
                services.Remove(descriptor);

                services.AddDbContext<CTP_TESTContext>(options => {
                    options.UseSqlite(_connection);
                });


                // Gets DbContext from startup class and seeds the In-Memory database
                ServiceProvider sp = services.BuildServiceProvider();
                using IServiceScope scope = sp.CreateScope();
                IServiceProvider scopedServices = scope.ServiceProvider;
                CTP_TESTContext db = scopedServices.GetRequiredService<CTP_TESTContext>();

                await db.Database.EnsureCreatedAsync();

                DBHelper.AddData(db);

                await db.SaveChangesAsync();
            });
        }
    }
}
