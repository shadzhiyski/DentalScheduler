using System.Threading.Tasks;

namespace DentalScheduler.Interfaces.Infrastructure.Common.Persistence
{
    public interface IUnitOfWork
    {
        int Save();

        Task<int> SaveAsync();
    }
}