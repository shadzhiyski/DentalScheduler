using System.Threading.Tasks;

namespace DentalScheduler.Interfaces.Gateways
{
    public interface IUnitOfWork
    {
        int Save();

        Task<int> SaveAsync();
    }
}