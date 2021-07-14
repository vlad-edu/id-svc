using System;
using IdService.Core.Constants;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace IdService.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfiguredDbContext(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (env == null) throw new ArgumentNullException(nameof(env));

            var builder = new SqlConnectionStringBuilder(configuration.GetConnectionString(ConnectionStrings.OwnDbConnection));
            var password = configuration.GetConnectionString(ConnectionStrings.OwnDbConnectionPassword);
            if (!string.IsNullOrEmpty(password)) builder.Password = password;

            services.AddDbContextPool<AppDbContext>(opt =>
                opt.UseSqlServer(builder.ConnectionString));

            services.AddDataProtection()
                .SetApplicationName($"{env.ApplicationName}-{env.EnvironmentName}")
                .PersistKeysToDbContext<AppDbContext>();

            return services;
        }

        public static IServiceCollection AddConfiguredQuartz(
            this IServiceCollection services)
        {
            services.Configure<QuartzOptions>(opt =>
            {
            });

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                q.UseTimeZoneConverter();
            });

            services.AddQuartzHostedService(opt =>
            {
                opt.WaitForJobsToComplete = true;
            });

            return services;
        }
    }
}
