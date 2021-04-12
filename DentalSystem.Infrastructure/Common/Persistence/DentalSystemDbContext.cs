using DentalSystem.Infrastructure.Identity.Persistence.Configurations;
using DentalSystem.Infrastructure.Scheduling.Persistence.Configurations;
using DentalSystem.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DentalSystem.Infrastructure.Common.Persistence
{
    public class DentalSystemDbContext : IdentityDbContext<User>
    {
        public DentalSystemDbContext(
            DbContextOptions<DentalSystemDbContext> options,
            Action<ModelBuilder> onModelCreated = null)
            : base(options)
        {
            OnModelCreated = onModelCreated;
        }

        public Action<ModelBuilder> OnModelCreated { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .EnableDetailedErrors();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new DentalWorkerConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new TreatmentConfiguration());
            modelBuilder.ApplyConfiguration(new TreatmentSessionConfiguration());
            modelBuilder.ApplyConfiguration(new DentalTeamConfiguration());
            modelBuilder.ApplyConfiguration(new DentalTeamParticipantConfiguration());

            OnModelCreated?.Invoke(modelBuilder);
        }
    }
}
