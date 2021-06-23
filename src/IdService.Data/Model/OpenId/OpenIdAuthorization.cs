using System;
using OpenIddict.EntityFrameworkCore.Models;

namespace IdService.Data.Model.OpenId
{
    public sealed class OpenIdAuthorization : OpenIddictEntityFrameworkCoreAuthorization<Guid, OpenIddictEntityFrameworkCoreApplication, OpenIdToken>
    {
    }
}
