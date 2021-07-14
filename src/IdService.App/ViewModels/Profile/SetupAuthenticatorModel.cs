using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace IdService.App.ViewModels.Profile
{
    public sealed class SetupAuthenticatorModel
    {
        [Required]
        [DisplayName("Code")]
        [RegularExpression(@"^\d{5,10}$", ErrorMessage = "Invalid Code")]
        public string? Code { get; set; }

        [HiddenInput]
        public string? SharedKey { get; set; }

        [HiddenInput]
        public string? RecoveryCodes { get; init; }

        [HiddenInput]
        public string? QrCodeSource { get; set; }
    }
}
