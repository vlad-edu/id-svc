using Microsoft.AspNetCore.Mvc;
using System;

namespace IdService.App.Controllers
{
    /// <summary>
    /// SignIn, SignOut.
    /// </summary>
    public partial class AccountController
    {
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmPassword()
        {
            return Model();
        }

        private static IActionResult Model()
        {
            throw new NotImplementedException();
        }
    }
}
