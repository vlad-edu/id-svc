using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdService.App.ViewModels.Account
{
    public sealed class ForgotPasswordModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        [DisplayName("Email address")]
        public string? Email { get; set; }
    }
}