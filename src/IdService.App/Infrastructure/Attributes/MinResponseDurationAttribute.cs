using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IdService.App.Infrastructure.Attributes
{
    public sealed class MinResponseDurationAttribute : ActionFilterAttribute
    {
        public MinResponseDurationAttribute(int milliseconds)
        {
            Milliseconds = milliseconds;
        }

        public int Milliseconds { get; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (next == null) throw new ArgumentNullException(nameof(next));

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await next();

            stopwatch.Stop();
            var diff = Milliseconds - stopwatch.Elapsed.TotalMilliseconds;
            if (diff > 0d) await Task.Delay(TimeSpan.FromMilliseconds(diff));
        }
    }
}
