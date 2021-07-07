using System;
using System.Threading.Tasks;
using MimeKit;
using MimeKit.Text;

namespace IdService.Services.Email
{
    internal sealed class EmailService : IEmailService
    {
        private readonly ISmtpSender _sender;

        public EmailService(ISmtpSender sender)
        {
            _sender = sender;
        }

        public async Task SendResetPasswordLinkAsync(Uri link, string email, string? user)
        {
            var message = new MimeMessage
            {
                Subject = $"Reset password to {link.Host}",
                Body = new TextPart(TextFormat.Plain) { Text = link.ToString() },
            };

            message.To.Add(new MailboxAddress(user ?? "User", email));
            await _sender.SendMessageAsync(message);
        }
    }
}
