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
            var model = new LoginViewModel
            {
                ReturnUrl = "http://google.com/",
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
            {
                ModelState.TryAddModelError(string.Empty, "Oops!");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, true);
            if (result.Succeeded) return RedirectToAction("Login");

            ModelState.TryAddModelError(string.Empty, result.ToString());
            return View(model);
        }
    }
}
