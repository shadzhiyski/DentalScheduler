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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserTable());
            modelBuilder.ApplyConfiguration(new RoomTable());
            modelBuilder.ApplyConfiguration(new DentalWorkerTable());
            modelBuilder.ApplyConfiguration(new PatientTable());
            modelBuilder.ApplyConfiguration(new TreatmentTable());
            modelBuilder.ApplyConfiguration(new TreatmentSessionTable());
            modelBuilder.ApplyConfiguration(new DentalTeamTable());
            modelBuilder.ApplyConfiguration(new DentalTeamParticipantTable());
        }
    }
}
