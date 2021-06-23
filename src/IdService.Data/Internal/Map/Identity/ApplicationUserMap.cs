using IdService.Data.Model.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdService.Data.Internal.Map.Identity
{
    internal sealed class ApplicationUserMap : EntityMappingConfiguration<ApplicationUser>
    {
        public override void Map(EntityTypeBuilder<ApplicationUser> b)
        {
            b.ToTable("Users", DbSchema.Identity).HasKey(t => t.Id).IsClustered(false);

            b.Property(t => t.UserName).IsUnicode(false);
            b.Property(t => t.NormalizedUserName).IsUnicode(false);
            b.Property(t => t.Email).IsUnicode(false);
            b.Property(t => t.NormalizedEmail).IsUnicode(false);
            b.Property(t => t.PasswordHash).IsUnicode(false);
            b.Property(t => t.SecurityStamp).IsUnicode(false);
            b.Property(t => t.ConcurrencyStamp).IsUnicode(false);
            b.Property(t => t.PhoneNumber).IsUnicode(false).HasMaxLength(256);
            b.Property(t => t.FirstName).HasMaxLength(256);
            b.Property(t => t.LastName).HasMaxLength(256);
            b.Property(t => t.Created).HasColumnType("DATETIME");
            b.Property(t => t.Description).IsUnicode(false);

            b.HasIndex(t => t.Created).IsUnique(false).IsClustered();
        }
    }
}
