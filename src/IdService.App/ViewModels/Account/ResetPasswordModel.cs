using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IdService.Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace IdService.App.ViewModels.Account
{
    public sealed class ResetPasswordModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        [DisplayName("Email address")]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("New Password")]
        [RegularExpression(RegexConstants.Password, ErrorMessage = "Invalid Password.")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare(nameof(NewPassword), ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [HiddenInput]
        public string? Token { get; set; }

        [HiddenInput]
        public Guid Id { get; set; }
    }
}