using System;
using IdService.Data;
using IdService.Data.Model.Identity;
using IdService.Services.Email;
using IdService.Services.Identity;
using IdService.Services.Providers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;

namespace IdService.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string EmailConfirmationTokenProviderName = "emailconfirmation";

        public static IServiceCollection AddConfiguredIdentity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
                {
                    configuration.Bind(nameof(IdentityOptions), opt);

                    opt.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
                    opt.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
                    opt.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;

                    opt.Tokens.EmailConfirmationTokenProvider = EmailConfirmationTokenProviderName;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders()
                .AddUserConfirmation<UserConfirmation<ApplicationUser>>()
                .AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>(EmailConfirmationTokenProviderName)
                .AddPasswordValidator<PasswordValidator<ApplicationUser>>();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                configuration.Bind(nameof(DataProtectionTokenProviderOptions), opt);
            });

            services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
            {
                configuration.Bind(nameof(EmailConfirmationTokenProviderOptions), opt);
            });

            return services;
        }

        public static IServiceCollection AddConfiguredServices(this IServiceCollection services)
        {
            services.AddSingleton<ISmtpSender, SmtpSender>();

            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
