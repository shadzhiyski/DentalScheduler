using System.Threading.Tasks;

namespace DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence
{
    public interface IUnitOfWork
    {
        int Save();

        Task<int> SaveAsync();
    }
}