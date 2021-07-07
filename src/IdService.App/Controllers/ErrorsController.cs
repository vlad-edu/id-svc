using System.Collections.Generic;
using System.Net;
using IdService.App.ViewModels.Errors;
using IdService.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdService.App.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public sealed class ErrorsController : AppController
    {
        private static readonly IDictionary<int, StatusCodeModel> KnownCodes = new Dictionary<int, StatusCodeModel>
        {
            {
                StatusCodes.Status400BadRequest,
                new StatusCodeModel
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Bad request",
                    Description = null,
                }
            },
            {
                StatusCodes.Status403Forbidden,
                new StatusCodeModel
                {
                    Code = StatusCodes.Status403Forbidden,
                    Message = "Access Denied.",
                    Description = null,
                }
            },
            {
                StatusCodes.Status404NotFound,
                new StatusCodeModel
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = "Page not found!",
                    Description = "The page you are looking for might have been removed had its name changed or is temporarily unavailable.",
                }
            },
            {
                StatusCodes.Status500InternalServerError,
                new StatusCodeModel
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Internal Server Error.",
                    Description = "Please try again later.",
                }
            },
        };

        private readonly ILogger<ErrorsController> _logger;

        public ErrorsController(
            ILogger<ErrorsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] int? status)
        {
            return status is < 500
                ? HandleStatusReExecute(status.Value)
                : HandleError(status ?? 500);
        }

        [HttpGet(nameof(Forbidden))]
        public IActionResult Forbidden()
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        private static StatusCodeModel CreateModel(int code)
        {
            if (!KnownCodes.TryGetValue(code, out var model))
            {
                model = new StatusCodeModel
                {
                    Code = code,
                    Message = ((HttpStatusCode)code).ToString(),
                };
            }

            return model;
        }

        private IActionResult HandleError(int status)
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (feature == null) return NotFound();
            using var d = _logger.EnrichScopeWith(feature);
            _logger.LogWarning($"Status code {status} page was displayed.");
            return View("Status", CreateModel(status));
        }

        private IActionResult HandleStatusReExecute(int status)
        {
            var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if (feature is null) return NotFound();
            using var d = _logger.EnrichScopeWith(feature);
            _logger.LogWarning($"Status code {status} page was displayed.");
            return View("Status", CreateModel(status));
        }
    }
}