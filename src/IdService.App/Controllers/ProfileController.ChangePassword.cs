using System;
using System.Threading.Tasks;
using IdService.App.ViewModels.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdService.App.Controllers
{
    /// <summary>
    /// Change password.
    /// </summary>
    public partial class ProfileController
    {
        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordModel());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Forbid();
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded) return RedirectToAction("Index", "Profile");
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Code);
            }

            return View(model);
        }
    }
}
