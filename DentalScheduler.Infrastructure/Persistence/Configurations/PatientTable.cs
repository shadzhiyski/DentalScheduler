using DentalScheduler.Infrastructure.Persistence.Helpers;
using DentalScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalScheduler.Infrastructure.Persistence.Configurations
{
    public class PatientTable : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.ReferenceId).HasValueGenerator<ReferenceIdGenerator>();
            
            builder.HasOne(e => e.IdentityUser);

            builder.Property(e => e.IdentityUserId).IsRequired();

            builder.HasIndex(e => e.IdentityUserId).IsUnique();
            builder.HasIndex(e => e.ReferenceId).IsUnique();
        }
    }
}