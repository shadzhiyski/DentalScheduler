using DentalScheduler.DAL.Constraints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DentalScheduler.DAL
{
    public class DentalSchedulerDbContext : IdentityDbContext<IdentityUser>
    {
        public DentalSchedulerDbContext(DbContextOptions<DentalSchedulerDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoomTable());
            modelBuilder.ApplyConfiguration(new DentistTable());
            modelBuilder.ApplyConfiguration(new PatientTable());
            modelBuilder.ApplyConfiguration(new TreatmentSessionTable());
            modelBuilder.ApplyConfiguration(new TreatmentSessionDoctorCommitmentTable());
        }
    }
}
