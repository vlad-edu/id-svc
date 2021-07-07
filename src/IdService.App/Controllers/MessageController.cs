using IdService.App.Infrastructure.Extensions;
using IdService.App.ViewModels.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdService.App.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public sealed class MessageController : AppController
    {
        [HttpGet]
        public IActionResult Message()
        {
            var model = TempData.Get<MessageModel>(MessageModel.TempDataKey);
            if (model == null) return NotFound();
            return View(model);
        }
    }
}
