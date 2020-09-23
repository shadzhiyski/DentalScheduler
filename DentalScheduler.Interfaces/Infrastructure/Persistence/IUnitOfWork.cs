using System.Threading.Tasks;

namespace DentalScheduler.Interfaces.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        int Save();

        Task<int> SaveAsync();
    }
}