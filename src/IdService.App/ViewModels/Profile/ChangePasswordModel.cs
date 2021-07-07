using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IdService.Core.Constants;

namespace IdService.App.ViewModels.Profile
{
    public sealed class ChangePasswordModel
    {
        [Required]
        [DisplayName("Old password")]
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [DisplayName("New password")]
        [DataType(DataType.Password)]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [RegularExpression(RegexConstants.Password, ErrorMessage = "Invalid Password.")]
        public string? NewPassword { get; set; }

        [Display(Name = "Confirm new password")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}