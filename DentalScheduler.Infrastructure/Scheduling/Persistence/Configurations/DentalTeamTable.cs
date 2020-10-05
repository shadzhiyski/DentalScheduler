using DentalScheduler.Infrastructure.Common.Persistence.Helpers;
using DentalScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalScheduler.Infrastructure.Scheduling.Persistence.Configurations
{
    public class DentalTeamTable : IEntityTypeConfiguration<DentalTeam>
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