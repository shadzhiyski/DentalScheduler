using DentalScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalScheduler.DAL.Constraints
{
    public class DentistTable : IEntityTypeConfiguration<Dentist>
    {
        public void Configure(EntityTypeBuilder<Dentist> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.HasOne(e => e.IdentityUser);

            builder.Property(e => e.IdentityUserId).IsRequired();
            builder.Property(e => e.JobType).IsRequired();

            builder.HasIndex(e => e.IdentityUserId).IsUnique(true);
        }
    }
}