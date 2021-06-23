using System;
using OpenIddict.EntityFrameworkCore.Models;

namespace IdService.Data.Model.OpenId
{
    public sealed class OpenIdToken : OpenIddictEntityFrameworkCoreToken<Guid, OpenIddictEntityFrameworkCoreApplication, OpenIdAuthorization>
    {
    }
}
