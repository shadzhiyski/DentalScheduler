using DentalScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalScheduler.DAL.Configurations
{
    public class DentalTeamTable : IEntityTypeConfiguration<DentalTeam>
    {
        public void Configure(EntityTypeBuilder<DentalTeam> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired();

            builder.HasIndex(e => e.Name).IsUnique();
        }
    }
}