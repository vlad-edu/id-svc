namespace IdService.Core.Options
{
    public sealed class SmtpOptions
    {
        public string? Host { get; set; }

        public int Port { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }
    }
}
