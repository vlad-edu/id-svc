using Microsoft.AspNetCore.Mvc;

namespace IdService.App.ViewModels.Profile
{
    public sealed class Manage2FaModel
    {
        [HiddenInput]
        public string? QrCode { get; set; }

        [HiddenInput]
        public string? ReturnUrl { get; set; }
    }
}
