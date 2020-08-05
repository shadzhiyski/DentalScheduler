using DentalScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalScheduler.DAL.Configurations
{
    public class TreatmentSessionTable : IEntityTypeConfiguration<TreatmentSession>
    {
        public void Configure(EntityTypeBuilder<TreatmentSession> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Patient);
            builder.HasOne(e => e.DentalTeam);
            builder.HasOne(e => e.Room);

            builder.Property(e => e.PatientId).IsRequired();
            builder.Property(e => e.DentalTeamId).IsRequired();
            builder.Property(e => e.RoomId).IsRequired();
            builder.Property(e => e.Start).IsRequired();
            builder.Property(e => e.End).IsRequired();

            builder.Property(e => e.Reason).HasMaxLength(256);

            builder.HasIndex(e => new { e.PatientId, e.DentalTeamId, e.RoomId, e.Start, e.End }).IsUnique();
        }
    }
}