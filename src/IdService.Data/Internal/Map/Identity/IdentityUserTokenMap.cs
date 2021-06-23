using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdService.Data.Internal.Map.Identity
{
    internal sealed class IdentityUserTokenMap : EntityMappingConfiguration<IdentityUserToken<Guid>>
    {
        public override void Map(EntityTypeBuilder<IdentityUserToken<Guid>> b)
        {
            b.ToTable("UserTokens", DbSchema.Identity);

            b.Property(t => t.LoginProvider).IsUnicode(false);
            b.Property(t => t.Name).IsUnicode(false);
            b.Property(t => t.Value).IsUnicode(false);
        }
    }
}
