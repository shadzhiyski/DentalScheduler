using DentalScheduler.Infrastructure.Identity.Persistence.Configurations;
using DentalScheduler.Infrastructure.Scheduling.Persistence.Configurations;
using DentalScheduler.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DentalScheduler.Infrastructure.Common.Persistence
{
    public class DentalSchedulerDbContext : IdentityDbContext<User>
    {
        public DentalSchedulerDbContext(DbContextOptions<DentalSchedulerDbContext> options)
            : base(options)
        { }

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
        }
    }
}
