using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdService.Data.Internal.Map.Identity
{
    internal sealed class IdentityUserLoginMap : EntityMappingConfiguration<IdentityUserLogin<Guid>>
    {
        public override void Map(EntityTypeBuilder<IdentityUserLogin<Guid>> b)
        {
            b.ToTable("UserLogins", DbSchema.Identity);

            b.Property(t => t.LoginProvider).IsUnicode(false);
            b.Property(t => t.ProviderKey).IsUnicode(false);
            b.Property(t => t.ProviderDisplayName).IsUnicode(false);
        }
    }
}
