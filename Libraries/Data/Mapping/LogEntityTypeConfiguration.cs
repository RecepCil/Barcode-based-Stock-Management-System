using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    internal class LogEntityTypeConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Logs");
            builder.HasKey(c => c.Id);

            //builder.HasOne(l => l.User)
            //  .WithMany(u => u.Logs)
            //  .HasForeignKey(l => l.UserId);
        }
    }
}