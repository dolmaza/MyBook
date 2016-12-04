using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Core.DB.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");

            HasKey(u => u.ID);

            Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("IX_UsernameUnique")
                        {
                            IsUnique = true
                        }));

            Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("nvarchar");

            Property(u => u.Firstname)
                .HasMaxLength(200)
                .HasColumnType("nvarchar");

            Property(u => u.Lastname)
                .HasMaxLength(200)
                .HasColumnType("nvarchar");

            HasRequired(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleID)
                .WillCascadeOnDelete(false);
        }
    }
}
