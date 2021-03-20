using System.Threading.Tasks;
using DentalSystem.Interfaces.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DentalSystem.Infrastructure.Common.Persistence
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