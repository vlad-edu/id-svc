using IdService.App.Infrastructure.Extensions;
using IdService.App.ViewModels.Message;
using Microsoft.AspNetCore.Mvc;

namespace IdService.App.Controllers
{
    public abstract class AppController : Controller
    {
        protected IActionResult ShowAlert(
            string message,
            MessageLevel? level = null,
            string? caption = null,
            string? description = null,
            string? actionName = null,
            string? actionUrl = null)
        {
            var model = new MessageModel { Message = message };
            if (level.HasValue) model.Level = level.Value;
            if (caption != null) model.Caption = caption;
            if (description != null) model.Description = description;
            if (actionName != null) model.ActionName = actionName;
            if (actionUrl != null) model.ActionUrl = actionUrl;

            TempData.Put(MessageModel.TempDataKey, model);
            return RedirectToAction("Message", "Message");
        }
    }
}
