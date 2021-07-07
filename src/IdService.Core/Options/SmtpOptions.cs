using System.ComponentModel.DataAnnotations;

namespace IdService.Core.Options
{
    public sealed class SmtpOptions
    {
        [Required]
        public string? Host { get; set; }

        [Range(1, ushort.MaxValue)]
        public int Port { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? FromName { get; set; }

        [Required]
        [EmailAddress]
        public string? FromAddress { get; set; }
    }
}
