using DentalSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalSystem.Infrastructure.Scheduling.Persistence.Configurations
{
    public class DentalTeamParticipantConfiguration
        : IEntityTypeConfiguration<DentalTeamParticipant>
    {
        public void Configure(EntityTypeBuilder<DentalTeamParticipant> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Team);
            builder.HasOne(e => e.Participant);

            builder.Property(e => e.TeamId).IsRequired();
            builder.Property(e => e.ParticipantId).IsRequired();

            builder.HasIndex(e => new { e.TeamId, e.ParticipantId }).IsUnique();
        }
    }
}