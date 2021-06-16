using System;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace IdService.TestClient
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Serilog.Debugging.SelfLog.Enable(Console.WriteLine);
            try
            {
                CreateHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.Fatal(ex, "Fatal error.");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseSerilog((context, configuration) =>
                        {
                            configuration.ReadFrom.Configuration(context.Configuration)
                                .Enrich.WithProperty("application", context.HostingEnvironment.ApplicationName)
                                .Enrich.WithProperty("environment", context.HostingEnvironment.EnvironmentName)
                                .Enrich.WithProperty("version", (Assembly.GetAssembly(typeof(Program)) ?? Assembly.GetExecutingAssembly()).GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion);
                        });
                });
        }
    }
}