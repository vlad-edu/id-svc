using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AspNetCore.ReCaptcha;
using IdService.App.Infrastructure.Extensions;
using IdService.App.ViewModels.Account;
using IdService.App.ViewModels.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdService.App.Controllers
{
    /// <summary>
    /// Forgot and reset password.
    /// </summary>
    public partial class AccountController
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateReCaptcha]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordModel model)
        {
            if (_signInManager.IsSignedIn(User)) await _signInManager.SignOutAsync();

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return ForgotPasswordMessage();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.ActionLink("ResetPassword", "Account", new { token = token.Base64Encode(), id = user.Id.ToString("N") });
            await _emailService.SendResetPasswordLinkAsync(new Uri(HtmlEncoder.Default.Encode(callbackUrl)), user.Email, user.FirstName);
            _logger.LogInformation("Reset password ling sent to user.");

            return ForgotPasswordMessage();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromRoute] Guid id, [FromQuery] string token)
        {
            if (id == Guid.Empty) return NotFound();

            throw new NotImplementedException();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword()
        {
            throw new NotImplementedException();
        }

        private IActionResult ForgotPasswordMessage()
        {
            return ShowAlert(
                "A link to reset your password should come to the specified email.",
                MessageLevel.Info,
                "Check mailbox",
                actionName: "OK",
                actionUrl: Url.Action("Index", "Home"));
        }
    }
}
