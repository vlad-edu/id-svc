using System;
using System.Threading.Tasks;
using IdService.Core.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace IdService.Services.Email
{
    internal sealed class SmtpSender : ISmtpSender
    {
        private readonly ILogger<SmtpSender> _logger;
        private readonly SmtpOptions _smtpOptions;

        public SmtpSender(
            IOptions<SmtpOptions> smtpOptionsAccessor,
            ILogger<SmtpSender> logger)
        {
            _logger = logger;
            _smtpOptions = smtpOptionsAccessor.Value;
        }

        public async Task<bool> SendMessageAsync(MimeMessage message)
        {
            using var client = new SmtpClient();
            message.From.Clear();
            message.From.Add(new MailboxAddress(_smtpOptions.FromName, _smtpOptions.FromAddress));
            try
            {
                await client.ConnectAsync(_smtpOptions.Host, _smtpOptions.Port, SecureSocketOptions.StartTlsWhenAvailable);
                client.AuthenticationMechanisms.Remove("XOAUTH");
                await client.AuthenticateAsync(_smtpOptions.Username, _smtpOptions.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                _logger.LogInformation("Mail {subject} sent to {to} successfully.", message.Subject, message.To[0].ToString());
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sending mail {subject} to {to} failed.", message.Subject, message.To[0].ToString());
                return false;
            }
        }
    }
}
