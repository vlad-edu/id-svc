using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IdService.Core.Constants;

namespace IdService.App.ViewModels.Account
{
    public sealed class RegisterModel
    {
        [Required(ErrorMessage = "Please enter Username.")]
        [Display(Name = "Username")]
        [RegularExpression(RegexConstants.Username, ErrorMessage = "Invalid Username.")]
        public string? Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        [DisplayName("Email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [RegularExpression(RegexConstants.Password, ErrorMessage = "Invalid Password.")]
        public string? Password { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
