using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AspNetCore.ReCaptcha;
using IdService.App.Infrastructure.Attributes;
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
        [MinResponseDuration(2_000)]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid) return View(model);

            if (_signInManager.IsSignedIn(User)) await _signInManager.SignOutAsync();

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return ForgotPasswordMessage();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.ActionLink("ResetPassword", "Account", new { token = token.Base64Encode(), id = user.Id.ToString("N") });
            await _emailService.SendResetPasswordLinkAsync(new Uri(HtmlEncoder.Default.Encode(callbackUrl)), user.Email, user.FirstName);
            _logger.LogInformation("Reset password link sent to user.");

            return ForgotPasswordMessage();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword([FromRoute] Guid id, [FromQuery] string? token)
        {
            if (id == Guid.Empty) return NotFound();
            if (token == null) return NotFound();

            var model = new ResetPasswordModel
            {
                Id = id,
                Token = token,
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [MinResponseDuration(2_000)]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (model.Id == Guid.Empty) return NotFound();
            if (model.Token == null) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user == null) return NotFound();

            var result = await _userManager.ResetPasswordAsync(user, model.Token.Base64Decode(), model.NewPassword);
            if (!result.Succeeded)
            {
                _logger.LogWarning("Reset password wasn't completed successfully. {errors}", result.Errors);
                return NotFound();
            }

            return ShowAlert(
                "Reset password was completed successfully.",
                MessageLevel.Success,
                "Well done",
                actionName: "OK",
                actionUrl: Url.Action("Index", "Profile"));
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
