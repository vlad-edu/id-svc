using IdService.Data.Model.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdService.Data.Internal.Map.Identity
{
    internal sealed class ApplicationRoleMap : EntityMappingConfiguration<ApplicationRole>
    {
        public override void Map(EntityTypeBuilder<ApplicationRole> b)
        {
            b.ToTable("Roles", DbSchema.Identity);

            b.Property(t => t.Name).IsUnicode(false);
            b.Property(t => t.NormalizedName).IsUnicode(false);
            b.Property(t => t.ConcurrencyStamp).IsUnicode(false);
        }
    }
}
