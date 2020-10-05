using System;
using DentalScheduler.Infrastructure.Common.Persistence.Helpers;
using DentalScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalScheduler.Infrastructure.Scheduling.Persistence.Configurations
{
    public class RoomTable : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.ReferenceId).HasValueGenerator<ReferenceIdGenerator>();

            builder.Property(e => e.Name).IsRequired();

            builder.HasIndex(e => e.ReferenceId).IsUnique();
        }
    }
}