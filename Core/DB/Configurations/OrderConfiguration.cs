using System.Data.Entity.ModelConfiguration;

namespace Core.DB.Configurations
{
    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            ToTable("Orders");

            HasKey(o => o.ID);

            Property(o => o.Firstname)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("nvarchar");

            Property(o => o.Lastname)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("nvarchar");

            Property(o => o.Address)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("nvarchar");

            Property(o => o.Mobile)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("nvarchar");

            Property(o => o.TotalPrice)
                .IsRequired()
                .HasColumnType("money");

            Property(o => o.Address)
                .HasColumnType("nvarchar");

            HasRequired(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserID)
                .WillCascadeOnDelete(false);

            HasRequired(o => o.Status)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.StatusID)
                .WillCascadeOnDelete(false);
        }
    }
}
