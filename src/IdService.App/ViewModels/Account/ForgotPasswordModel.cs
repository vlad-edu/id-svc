using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IdService.Core.Constants;

namespace IdService.App.ViewModels.Account
{
    public sealed class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Please enter Username.")]
        [Display(Name = "Username")]
        [RegularExpression(RegexConstants.Username, ErrorMessage = "Invalid Username.")]
        public string? Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        [DisplayName("Email address")]
        public string? Email { get; set; }
    }
}