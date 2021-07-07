using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace IdService.App.Infrastructure.Extensions
{
    internal static class TempDataDictionaryExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value)
            where T : class, new()
        {
            tempData[key] = JsonSerializer.Serialize(value);
        }

        public static T? Get<T>(this ITempDataDictionary tempData, string key)
            where T : class, new()
        {
            tempData.TryGetValue(key, out var o);
            return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
        }
    }
}
