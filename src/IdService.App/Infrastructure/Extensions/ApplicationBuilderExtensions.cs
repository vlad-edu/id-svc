using System;
using System.Linq;
using IdService.Core.Constants;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.AspNetCore;
using Serilog.Events;

namespace IdService.App.Infrastructure.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds middleware for streamlined request logging. Instead of writing HTTP request information
        /// like method, path, timing, status code and exception details
        /// in several events, this middleware collects information during the request (including from
        /// <see cref="IDiagnosticContext" />), and writes a single event at request completion. Add this
        /// in <c>Startup.cs</c> before any handlers whose activities should be logged.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <returns>The application builder.</returns>
        public static IApplicationBuilder UseDiagnosticLogging(this IApplicationBuilder app)
        {
            return app.UseSerilogRequestLogging(RequestLoggerConfigurator);
        }

        private static void RequestLoggerConfigurator(RequestLoggingOptions opt)
        {
            opt.GetLevel = (ctx, _, ex) => ex != null
                ? LogEventLevel.Error
                : ctx.Request.Path.Equals(HostingConstants.HealthCheckEndpoint, StringComparison.OrdinalIgnoreCase)
                      || ctx.Request.Path.Equals(HostingConstants.MetricsEndpoint, StringComparison.OrdinalIgnoreCase)
                    ? LogEventLevel.Verbose
                    : LogEventLevel.Information;

            opt.EnrichDiagnosticContext = (context, httpContext) =>
            {
                context.Set(
                    "Headers",
                    httpContext.Request.Headers
                        .Where(h => h.Key.StartsWith("X-", StringComparison.OrdinalIgnoreCase))
                        .ToDictionary(k => k.Key, v => v.Value.ToString()),
                    true);

                context.Set("User", httpContext.User.Identity?.Name ?? "?");
            };
        }
    }
}
