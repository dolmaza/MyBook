using System.Data.Entity.ModelConfiguration;

namespace Core.DB.Configurations
{
    public class DictionaryConfiguration : EntityTypeConfiguration<Dictionary>
    {
        public DictionaryConfiguration()
        {
            ToTable("Dictionaries");

            HasKey(d => d.ID);

            Property(d => d.Caption)
                .IsRequired()
                .HasMaxLength(200);

            Property(d => d.CaptionEng)
                .HasMaxLength(200);

            Property(d => d.DecimalValue)
                .HasColumnType("money");

            Property(d => d.Code)
                .IsRequired();

            Property(d => d.StringCode)
                .HasMaxLength(200);

            HasOptional(d => d.Parent)
                .WithMany(d => d.Childrens)
                .HasForeignKey(d => d.ParentID)
                .WillCascadeOnDelete(false);
        }
    }
}
