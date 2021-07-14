using System;
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
            Uri? actionUri = null)
        {
            var model = new MessageModel { Message = message };
            if (level.HasValue) model.Level = level.Value;
            if (caption != null) model.Caption = caption;
            if (description != null) model.Description = description;
            if (actionName != null) model.ActionName = actionName;
            if (actionUri != null) model.ActionUrl = actionUri.ToString();

            TempData.Put(MessageModel.TempDataKey, model);
            return RedirectToAction("Message", "Message");
        }
    }
}
