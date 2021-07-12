using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AspNetCore.ReCaptcha;
using IdService.App.Infrastructure.Extensions;
using IdService.App.ViewModels.Account;
using IdService.App.ViewModels.Message;
using IdService.Data.Model.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdService.App.Controllers
{
    /// <summary>
    /// SignUp.
    /// </summary>
    public partial class AccountController
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateReCaptcha]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                user = await _userManager.FindByNameAsync(model.Username);
                if (user == null) return NotFound();

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.ActionLink("ConfirmEmail", "Account", new { token = token.Base64Encode(), id = user.Id.ToString("N") });
                await _emailService.SendConfirmEmailAsync(new Uri(HtmlEncoder.Default.Encode(callbackUrl)), user.Email, user.FirstName);
                _logger.LogInformation("Confirm email link has been sent to user.");

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromRoute] Guid id, [FromQuery] string? token)
        {
            if (id == Guid.Empty) return NotFound();
            if (token == null) return NotFound();

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();

            var result = await _userManager.ConfirmEmailAsync(user, token.Base64Decode());
            if (!result.Succeeded)
            {
                _logger.LogWarning("Email confirm wasn't completed successfully. {errors}", result.Errors);
                return NotFound();
            }

            _logger.LogInformation("Email confirm was completed successfully. {@user}", user);

            return ShowAlert(
                "Email confirm was completed successfully.",
                MessageLevel.Success,
                "Well done",
                actionName: "OK",
                actionUrl: Url.Action("Index", "Home"));
        }
    }
}
