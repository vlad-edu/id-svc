using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdService.Data.Internal.Map.Identity
{
    internal sealed class IdentityUserRoleMap : EntityMappingConfiguration<IdentityUserRole<Guid>>
    {
        public override void Map(EntityTypeBuilder<IdentityUserRole<Guid>> b)
        {
            b.ToTable("UserRoles", DbSchema.Identity);
        }
    }
}
