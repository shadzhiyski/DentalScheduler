using DentalSystem.Domain.Scheduling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalSystem.Infrastructure.Scheduling.Persistence.Configurations
{
    public class DentalWorkerConfiguration : IEntityTypeConfiguration<DentalWorker>
    {
        public void Configure(EntityTypeBuilder<DentalWorker> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.IdentityUser);

            builder.Property(e => e.IdentityUserId).IsRequired();
            builder.Property(e => e.JobType).IsRequired();

            builder.HasIndex(e => e.IdentityUserId).IsUnique(true);
        }
    }
}