using System.Data.Entity.ModelConfiguration;

namespace Core.DB.Configurations
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            ToTable("Roles");

            HasKey(r => r.ID);

            Property(r => r.Caption)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("nvarchar");

            HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .Map(m =>
                {
                    m.ToTable("RolePermissions");
                    m.MapLeftKey("RoleID");
                    m.MapRightKey("PermissionID");
                });
        }
    }
}
