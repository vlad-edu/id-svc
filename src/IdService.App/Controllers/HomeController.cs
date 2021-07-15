using Microsoft.AspNetCore.Mvc;

namespace IdService.App.Controllers
{
    public sealed class HomeController : AppController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
