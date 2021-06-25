using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IdService.Core.Extensions
{
    public static class LoggerExtensions
    {
        public static IDisposable EnrichScopeWith([NotNull] this ILogger logger, [NotNull] HttpRequest request, [NotNull] object obj, [CallerMemberName] string eventSource = "")
        {
            if (logger is null) throw new ArgumentNullException(nameof(logger));
            if (request is null) throw new ArgumentNullException(nameof(request));
            if (obj is null) throw new ArgumentNullException(nameof(obj));

            return logger.BeginScope(FromRequest(request)
                .Prepend(new KeyValuePair<string, object>(nameof(eventSource), eventSource))
                .Union(FromObject(obj)));
        }

        public static IDisposable EnrichScopeWith([NotNull] this ILogger logger, [NotNull] object obj, [CallerMemberName] string eventSource = "")
        {
            if (logger is null) throw new ArgumentNullException(nameof(logger));
            if (obj is null) throw new ArgumentNullException(nameof(obj));

            return logger.BeginScope(FromObject(obj)
                .Prepend(new KeyValuePair<string, object>(nameof(eventSource), eventSource)));
        }

        public static IDisposable EnrichScopeWith([NotNull] this ILogger logger, [NotNull] HttpRequest request, [CallerMemberName] string eventSource = "")
        {
            if (logger is null) throw new ArgumentNullException(nameof(logger));
            if (request is null) throw new ArgumentNullException(nameof(request));

            return logger.BeginScope(FromRequest(request)
                .Prepend(new KeyValuePair<string, object>(nameof(eventSource), eventSource)));
        }

        private static IEnumerable<KeyValuePair<string, object>> FromObject<T>(T obj)
            where T : notnull
        {
            return obj.GetType()
                .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .Select(p => new KeyValuePair<string, object>($"{obj.GetType().Name}.{p.Name}", p.GetValue(obj) ?? string.Empty));
        }

        private static IEnumerable<KeyValuePair<string, object>> FromRequest(HttpRequest request)
        {
            return request.Headers.Where(h => !h.Key.Equals("Cookie", StringComparison.OrdinalIgnoreCase))
                .Select(h => new KeyValuePair<string, object>($"Header.{h.Key}", h.Value.ToString()))
                .Prepend(new KeyValuePair<string, object>(nameof(request.Method), request.Method));
        }
    }
}
