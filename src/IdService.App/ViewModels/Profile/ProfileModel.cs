using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdService.App.ViewModels.Profile
{
    public sealed class ProfileModel
    {
        [Required]
        [DisplayName("First Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string? FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string? LastName { get; set; }

        [DisplayName("Email")]
        public string? Email { get; set; }

        [DisplayName("Phone Number")]
        public string? Phone { get; set; }

        [DisplayName("Status")]
        public string? Status { get; set; }
    }
}
