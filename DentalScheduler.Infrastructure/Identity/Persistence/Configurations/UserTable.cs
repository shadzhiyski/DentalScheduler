using DentalScheduler.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalScheduler.Infrastructure.Identity.Persistence.Configurations
{
    public class UserTable : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.FirstName).HasMaxLength(32);

            builder.Property(e => e.LastName).HasMaxLength(32);
        }
    }
}