using System;
using OpenIddict.EntityFrameworkCore.Models;

namespace IdService.Data.Model.OpenId
{
    public sealed class OpenIdApplication : OpenIddictEntityFrameworkCoreApplication<Guid, OpenIddictEntityFrameworkCoreAuthorization, OpenIdToken>
    {
    }
}
