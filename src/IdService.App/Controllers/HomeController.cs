using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdService.App.Controllers
{
    public partial class HomeController : Controller
    {
        [HttpGet]
        [AllowAnonymous]

        public IActionResult Index()
        {
            return View();
        }
    }
}
