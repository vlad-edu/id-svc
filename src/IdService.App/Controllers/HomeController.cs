using IdService.App.ViewModels.Message;
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

        [HttpGet]
        public IActionResult ShowMessage()
        {
            return ShowAlert(
                "Aww yeah, you successfully read this important alert message. This example text is going to run a bit longer so that you can see how spacing within an alert works with this kind of content.",
                MessageLevel.Success,
                "Well done!",
                "Whenever you need to, be sure to use margin utilities to keep things nice and tidy.",
                "Link");
        }
    }
}
