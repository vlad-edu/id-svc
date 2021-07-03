using IdService.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdService.App.Infrastructure.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<SmtpOptions>()
                .Bind(configuration.GetSection(nameof(SmtpOptions)))
                .ValidateDataAnnotations();

            return services;
        }
    }
}
