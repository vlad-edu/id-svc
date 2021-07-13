using System;
using System.Threading.Tasks;
using IdService.App.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdService.App.Controllers
{
    /// <summary>
    /// SignIn, SignOut.
    /// </summary>
    public partial class AccountController
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var model = new LoginModel
            {
                ReturnUrl = "~/",
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
            {
                ModelState.TryAddModelError(string.Empty, "Oops!");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, true);
            if (result.RequiresTwoFactor) return RedirectToAction("LoginWith2Fa");
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "/");
            }

            ModelState.TryAddModelError(string.Empty, result.ToString());
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2Fa()
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null) return NotFound();

            var model = new LoginWith2FaModel
            {
                ReturnUrl = "~/",
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2Fa([FromForm] LoginWith2FaModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null) return NotFound();

            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(model.Code, true, model.RememberMachine);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "The code is incorrect");
                return View(model);
            }

            return RedirectToAction("Index", "Profile");
        }
    }
}
