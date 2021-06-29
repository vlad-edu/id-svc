using System.ComponentModel.DataAnnotations;
using IdService.Core.Constants;

namespace IdService.App.ViewModels.Profile
{
    public sealed class ProfileModel
    {
        [Required(ErrorMessage = "Please enter new password.")]
        [Display(Name = "New password")]
        [RegularExpression(RegexConstants.Username, ErrorMessage = "Invalid password.")]
        public string? NewPassword { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
