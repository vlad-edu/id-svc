using System;
using IdService.Core.Constants;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdService.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfiguredDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var builder = new SqlConnectionStringBuilder(configuration.GetConnectionString(ConnectionStrings.OwnDbConnection));
            var password = configuration.GetConnectionString(ConnectionStrings.OwnDbConnectionPassword);
            if (!string.IsNullOrEmpty(password)) builder.Password = password;

            services.AddDbContextPool<AppDbContext>(opt => opt.UseSqlServer(builder.ConnectionString));
            return services;
        }
    }
}
