using DentalScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalScheduler.DAL.Configurations
{
    public class RoomTable : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired();
        }
    }
}