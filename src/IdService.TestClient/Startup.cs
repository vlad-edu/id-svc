using IdService.Core.Constants;
using IdService.Core.Exceptions;
using IdService.TestClient.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;

namespace IdService.TestClient
{
    public sealed class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly string _managementPort;

        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
            _managementPort = _configuration[HostingConstants.ManagementPortConfigurationKey] ?? throw new InvalidConfigurationException("Invalid configuration.", HostingConstants.ManagementPortConfigurationKey);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsDevelopment()) services.AddApplicationInsightsTelemetry();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor
                    | ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            services.AddHealthChecks();
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseForwardedHeaders();

            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseStatusCodePagesWithReExecute(HostingConstants.ErrorEndpoint, "?status={0}");
            app.UseStaticFiles();
            app.UseDiagnosticLogging();
            app.UseRouting();
            app.UseHttpMetrics();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks(HostingConstants.HealthCheckEndpoint).RequireHost($"*:{_managementPort}");
                endpoints.MapMetrics(HostingConstants.MetricsEndpoint).RequireHost($"*:{_managementPort}");
                endpoints.MapDefaultControllerRoute();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("ID Service test client");
                });
            });
        }
    }
}