using DentalScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalScheduler.DAL.Constraints
{
    public class PatientTable : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.HasOne(e => e.IdentityUser);

            builder.Property(e => e.IdentityUserId).IsRequired();

            builder.HasIndex(e => e.IdentityUserId).IsUnique(true);
        }
    }
}