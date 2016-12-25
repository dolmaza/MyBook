using System.Data.Entity.ModelConfiguration;

namespace Core.DB.Configurations
{
    public class ClientConfiguration : EntityTypeConfiguration<Client>
    {
        public ClientConfiguration()
        {
            ToTable("Clients");

            HasKey(c => c.ID);

            Property(c => c.Firstname)
                .IsRequired()
                .HasMaxLength(200);

            Property(c => c.Lastname)
                .IsRequired()
                .HasMaxLength(200);

            Property(c => c.Mobile)
                .IsRequired()
                .HasMaxLength(20);

            HasRequired(c => c.Status)
                .WithMany(s => s.Clients)
                .HasForeignKey(c => c.StatusID)
                .WillCascadeOnDelete(false);

            HasMany(o => o.Orders)
                .WithRequired(c => c.Client)
                .HasForeignKey(o => o.ClientID)
                .WillCascadeOnDelete(false);

            HasRequired(c => c.User)
                .WithMany(u => u.Clients)
                .HasForeignKey(c => c.UserID)
                .WillCascadeOnDelete(false);
        }
    }
}
