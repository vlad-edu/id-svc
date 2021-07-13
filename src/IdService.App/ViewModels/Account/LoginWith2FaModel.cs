using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace IdService.App.ViewModels.Account
{
    public class LoginWith2FaModel
    {

        [Required]
        [DisplayName("Code")]
        [RegularExpression(@"^\d{5,10}$", ErrorMessage = "Enter a 5 to 10 character code")]
        public string? Code { get; set; }

        [DisplayName("Remember me")]
        public bool RememberMachine { get; set; }

        [HiddenInput]
        public string? ReturnUrl { get; set; }
    }
}
