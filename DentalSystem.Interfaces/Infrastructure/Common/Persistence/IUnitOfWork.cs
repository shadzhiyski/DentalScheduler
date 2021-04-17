using System.Threading.Tasks;

namespace DentalSystem.Boundaries.Infrastructure.Common.Persistence
{
    public interface IUnitOfWork
    {
        int Save();

        Task<int> SaveAsync();
    }
}