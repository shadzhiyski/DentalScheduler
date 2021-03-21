using System.Threading.Tasks;

namespace DentalSystem.Interfaces.Infrastructure.Common.Persistence
{
    public interface IInitializer
    {
        Task Initialize();
    }
}