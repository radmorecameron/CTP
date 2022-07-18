using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics.CodeAnalysis;
using Serilog;

namespace CodeTestingPlatform {
    [ExcludeFromCodeCoverageAttribute]
    public class Program {
        public static int Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                .CreateLogger();

            Log.Information("Starting up!");

            try {
                CreateHostBuilder(args).Build().Run();

                Log.Information("Stopped cleanly");
                return 0;

            }
            catch (Exception ex) {
                Log.ForContext<Program>().Fatal(ex, $"Host terminated unexpectedly");
                return 1;

            }
            finally {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services))
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
