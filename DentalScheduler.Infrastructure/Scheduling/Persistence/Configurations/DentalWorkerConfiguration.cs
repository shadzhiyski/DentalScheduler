using DentalScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalScheduler.Infrastructure.Scheduling.Persistence.Configurations
{
    public class DentalWorkerConfiguration : IEntityTypeConfiguration<Entities.DentalWorker>
    {
        public void Configure(EntityTypeBuilder<Entities.DentalWorker> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.HasOne(e => e.IdentityUser);

            builder.Property(e => e.IdentityUserId).IsRequired();
            builder.Property(e => e.JobType).IsRequired();

            builder.HasIndex(e => e.IdentityUserId).IsUnique(true);
        }
    }
}