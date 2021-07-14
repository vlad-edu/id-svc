using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdService.Data.Internal.Map.DataProtection
{
    internal sealed class DataProtectionKeyMap : EntityMappingConfiguration<DataProtectionKey>
    {
        public override void Map(EntityTypeBuilder<DataProtectionKey> b)
        {
            b.ToTable("DataProtectionKeys", DbSchema.DataProtection);

            b.Property(t => t.FriendlyName).IsUnicode(false);
            b.Property(t => t.Xml).IsUnicode(false);
        }
    }
}
