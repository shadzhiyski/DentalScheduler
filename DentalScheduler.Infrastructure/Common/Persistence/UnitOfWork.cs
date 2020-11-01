using System.Threading.Tasks;
using DentalScheduler.Interfaces.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DentalScheduler.Infrastructure.Common.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext DbContext { get; }

        public UnitOfWork(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public int Save()
        {
            return DbContext.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return DbContext.SaveChangesAsync();
        }
    }
}