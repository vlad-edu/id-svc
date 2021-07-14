using System.Drawing;
using System.Globalization;
using QRCoder;

namespace IdService.App.Infrastructure.Helpers
{
    internal static class QrCodeGenerator
    {
        private const string HtmlDataFormat = "data:image/{0};base64,{1}";
        private const Base64QRCode.ImageType ImageType = Base64QRCode.ImageType.Png;

        public static string GetTotpBase64Source(string host, string code, string label)
        {
            var payload = new PayloadGenerator.OneTimePassword
            {
                Secret = code,
                Issuer = host,
                Label = label,
            }.ToString();

            using var qrGenerator = new QRCodeGenerator();
            var qrData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new Base64QRCode(qrData);
            var qrCodeBase64Image = qrCode.GetGraphic(20, Color.Black, Color.White, true, ImageType);

#pragma warning disable CA1308 // Normalize strings to uppercase
            return string.Format(CultureInfo.InvariantCulture, HtmlDataFormat, ImageType.ToString().ToLowerInvariant(), qrCodeBase64Image);
#pragma warning restore CA1308 // Normalize strings to uppercase
        }
    }
}
