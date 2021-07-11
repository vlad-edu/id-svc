using System;
using System.Threading.Tasks;

namespace IdService.Services.Email
{
    public interface IEmailService
    {
        Task SendConfirmEmailAsync(Uri link, string email, string? user);

        Task SendResetPasswordLinkAsync(Uri link, string email, string? user);
    }
}
