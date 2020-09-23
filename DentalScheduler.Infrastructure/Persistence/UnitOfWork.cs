using System.Threading.Tasks;
using DentalScheduler.Interfaces.Gateways;
using Microsoft.EntityFrameworkCore;

namespace DentalScheduler.DAL
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