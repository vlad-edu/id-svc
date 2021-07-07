using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace IdService.App.Infrastructure.Extensions
{
    internal static class StringExtensions
    {
        public static string Base64Decode(this string text)
        {
            return Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(text));
        }

        public static string Base64Encode(this string text)
        {
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(text));
        }
    }
}
