using System.IO;
using System.Reflection;
using IdService.Core.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IdService.Data
{
    public sealed class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString(ConnectionStrings.OwnDbConnection);
            var migrationAssemblyName = Assembly.GetAssembly(typeof(AppDbContext))?.GetName().Name;

            builder.UseSqlServer(connectionString, opt => opt.MigrationsAssembly(migrationAssemblyName));

            return new AppDbContext(builder.Options);
        }
    }
}
