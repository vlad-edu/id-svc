using System;
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
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login([FromForm] LoginViewModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            if (ModelState.IsValid) return RedirectToAction("Login");

            ModelState.AddModelError(string.Empty, "Oops! We don't see a user with that login info. Please try again.");
            return View(model);
        }
    }
}
