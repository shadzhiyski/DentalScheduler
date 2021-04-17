using DentalSystem.Infrastructure.Common.Persistence.Helpers;
using DentalSystem.Entities.Scheduling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalSystem.Infrastructure.Scheduling.Persistence.Configurations
{
    public class DentalTeamConfiguration : IEntityTypeConfiguration<DentalTeam>
    {
        public void Configure(EntityTypeBuilder<DentalTeam> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.ReferenceId).HasValueGenerator<ReferenceIdGenerator>();

            builder.HasOne(e => e.Room);

            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.RoomId).IsRequired();

            builder.HasIndex(e => e.Name).IsUnique();
            builder.HasIndex(e => e.ReferenceId).IsUnique();
        }
    }
}