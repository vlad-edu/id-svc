using System;
using System.Threading.Tasks;

namespace IdService.Services.Email
{
    public interface IEmailService
    {
        Task SendResetPasswordLinkAsync(Uri link, string email, string? user);
    }
}
