using Microsoft.AspNetCore.Mvc;

namespace IdService.App.Controllers
{
    /// <summary>
    /// Common members.
    /// </summary>
    [AutoValidateAntiforgeryToken]
    public sealed partial class AccountController : Controller
    {
        public AccountController()
        {
        }
    }
}
