using System;
using System.Threading.Tasks;
using IdService.App.ViewModels.Profile;
using IdService.Data.Model.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdService.App.Controllers
{
    [AutoValidateAntiforgeryToken]
    public sealed class ProfileController : Controller
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
    }
}
