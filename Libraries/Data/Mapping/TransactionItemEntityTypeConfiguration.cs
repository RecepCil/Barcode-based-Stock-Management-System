using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    internal class TransactionItemEntityTypeConfiguration : IEntityTypeConfiguration<TransactionItem>
    {
        public void Configure(EntityTypeBuilder<TransactionItem> builder)
        {
            builder.ToTable("TransactionItems");
            builder.HasKey(c => c.Id);

            builder.HasOne(i => i.Transaction)
              .WithMany(t => t.TransactionItems)
              .HasForeignKey(i => i.TransactionId);
        }
    }
}