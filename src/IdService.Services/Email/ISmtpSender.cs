using System.Threading.Tasks;
using MimeKit;

namespace IdService.Services.Email
{
    public interface ISmtpSender
    {
        Task<bool> SendMessageAsync(MimeMessage message);
    }
}
