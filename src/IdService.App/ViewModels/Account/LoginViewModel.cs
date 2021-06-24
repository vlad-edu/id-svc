using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace IdService.App.ViewModels.Account
{
    public sealed class LoginViewModel
    {
        [Required]
        [DisplayName("Username")]

        public string? Username { get; set; }

        [Required]
        [DisplayName("Password")]
        [MinLength(8, ErrorMessage = "Password is too short")]
        [MaxLength(48, ErrorMessage = "Password is too long")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DisplayName("Remember me")]
        public bool RememberLogin { get; set; }

        [HiddenInput]
        public string? ReturnUrl { get; set; }
    }
}
