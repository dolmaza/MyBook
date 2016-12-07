using System.Data.Entity.ModelConfiguration;

namespace Core.DB.Configurations
{
    public class PermissionConfiguration : EntityTypeConfiguration<Permission>
    {
        public PermissionConfiguration()
        {
            ToTable("Permissions");

            HasKey(p => p.ID);

            Property(p => p.Caption)
                .HasColumnType("nvarchar")
                .IsRequired()
                .HasMaxLength(200);

            Property(p => p.Url)
                .IsRequired()
                .HasMaxLength(200);

            Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(50);
            HasOptional(p => p.Parent)
                .WithMany(p => p.Childrens)
                .HasForeignKey(p => p.ParentID)
                .WillCascadeOnDelete(false);
        }
    }
}
