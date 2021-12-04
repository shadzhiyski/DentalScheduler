using System;
using DentalSystem.Infrastructure.Common.Persistence.Helpers;
using DentalSystem.Domain.Scheduling.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalSystem.Infrastructure.Scheduling.Persistence.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
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