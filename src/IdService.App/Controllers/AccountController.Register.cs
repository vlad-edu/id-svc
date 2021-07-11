using System;
using System.Threading.Tasks;
using AspNetCore.ReCaptcha;
using IdService.App.ViewModels.Account;
using IdService.Data.Model.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            if (result.Succeeded) return RedirectToAction("Login");

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

            throw new NotImplementedException();
        }
    }
}
