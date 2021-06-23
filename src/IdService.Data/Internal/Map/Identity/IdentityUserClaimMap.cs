using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdService.Data.Internal.Map.Identity
{
    internal sealed class IdentityUserClaimMap : EntityMappingConfiguration<IdentityUserClaim<Guid>>
    {
        public override void Map(EntityTypeBuilder<IdentityUserClaim<Guid>> b)
        {
            b.ToTable("UserClaims", DbSchema.Identity);

            b.Property(t => t.ClaimType).IsUnicode(false);
            b.Property(t => t.ClaimValue).IsUnicode(false);
        }
    }
}
