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

        public async Task SendConfirmEmailAsync(Uri link, string email, string? user)
        {

            var message = new MimeMessage
            {
                Subject = $"Confirm email to {link.Host}",
                Body = new TextPart(TextFormat.Html)
                {
                    Text = $"<p>Link for your registration. Go throw the link and complete the registration process.</p> <ul>  <li>  <a href={link}>Complete the registration</a> </li> </ul>",
                },
            };

            message.To.Add(new MailboxAddress(user ?? "User", email));
            await _sender.SendMessageAsync(message);
        }

        public async Task SendResetPasswordLinkAsync(Uri link, string email, string? user)
        {
            var message = new MimeMessage
            {
                Subject = $"Confirm email to {link.Host}",
                Body = new TextPart(TextFormat.Html)
                {
                    Text = $"<p>Link fo resetting password. Go throw the link and complete the resetting password process.</p> <ul>  <li>  <a href={link}>Complete the resetting password</a> </li> </ul>",
                },
            };

            message.To.Add(new MailboxAddress(user ?? "User", email));
            await _sender.SendMessageAsync(message);
        }
    }
}