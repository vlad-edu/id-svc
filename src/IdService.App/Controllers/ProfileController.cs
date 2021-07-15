using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdService.App.Infrastructure.Helpers;
using IdService.App.ViewModels.Profile;
using IdService.Data.Model.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdService.App.Controllers
{
    /// <summary>
    /// View and manipulation profile.
    /// </summary>
    [AutoValidateAntiforgeryToken]
    public sealed partial class ProfileController : AppController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewData["IsTwoFactorEnabled"] = await _userManager.GetTwoFactorEnabledAsync(user);
            var model = new ProfileModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Status = user.Status.ToString(),
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Index([FromForm] ProfileModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SetupAuthenticator()
        {
            var user = await _userManager.GetUserAsync(User);
            if (await _userManager.GetTwoFactorEnabledAsync(user)) return BadRequest();

            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            var email = await _userManager.GetEmailAsync(user);
            var model = new SetupAuthenticatorModel
            {
                QrCodeSource = QrCodeGenerator.GetTotpBase64Source(Request.Host.Host, unformattedKey, email),
                SharedKey = FormatKey(unformattedKey),
                RecoveryCodes = FormatCodes((await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10)).ToList(), 2),
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SetupAuthenticator([FromForm] SetupAuthenticatorModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            var isValid = await _userManager.VerifyTwoFactorTokenAsync(
                user,
                _userManager.Options.Tokens.AuthenticatorTokenProvider,
                model.Code);

            if (!isValid)
            {
                ModelState.AddModelError(nameof(model.Code), "Verification code is invalid.");
                return View(model);
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Disable2Fa()
        {
            var user = await _userManager.GetUserAsync(User);
            if (!await _userManager.GetTwoFactorEnabledAsync(user)) return BadRequest();
            await _userManager.SetTwoFactorEnabledAsync(user, false);
            return RedirectToAction("Index");
        }

        private static string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            var currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(' ');
                currentPosition += 4;
            }

            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey[currentPosition..]);
            }

#pragma warning disable CA1308 // Normalize strings to uppercase
            return result.ToString().ToLowerInvariant();
#pragma warning restore CA1308 // Normalize strings to uppercase
        }

        private static string FormatCodes(IList<string> codes, int columns)
        {
            if (columns <= default(int)) throw new ArgumentException($"Parameter {nameof(columns)} must be greater than 0.");
            var result = new StringBuilder();
            for (var i = 0; i < codes.Count; i++)
            {
                var column = i + 1;
                if (column % columns > 0) result.Append($"{codes[i]} ");
                else result.AppendLine(codes[i]);
            }

            return result.ToString();
        }
    }
}
