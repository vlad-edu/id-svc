using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdService.Data.Internal.Map.Identity
{
    internal sealed class IdentityRoleClaimMap : EntityMappingConfiguration<IdentityRoleClaim<Guid>>
    {
        public override void Map(EntityTypeBuilder<IdentityRoleClaim<Guid>> b)
        {
            b.ToTable("RoleClaims", DbSchema.Identity);

            b.Property(t => t.ClaimType).IsUnicode(false);
            b.Property(t => t.ClaimValue).IsUnicode(false);
        }
    }
}
