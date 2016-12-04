using System.Data.Entity.ModelConfiguration;

namespace Core.DB.Configurations
{
    public class OrderDetailConfiguration : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailConfiguration()
        {
            ToTable("OrderDetails");

            HasKey(od => od.ID);

            Property(od => od.BookName)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("nvarchar");

            Property(od => od.Price)
                .IsRequired()
                .HasColumnType("money");

            HasRequired(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID)
                .WillCascadeOnDelete(true);
        }
    }
}
