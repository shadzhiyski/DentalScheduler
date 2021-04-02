using System.Threading.Tasks;

namespace DentalSystem.Interfaces.Infrastructure.Common.Persistence
{
    public interface IUnitOfWork
    {
        int Save();

        Task<int> SaveAsync();
    }
}