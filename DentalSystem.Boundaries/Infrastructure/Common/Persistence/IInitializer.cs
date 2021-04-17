using System.Threading.Tasks;

namespace DentalSystem.Boundaries.Infrastructure.Common.Persistence
{
    public interface IInitializer
    {
        Task Initialize();
    }
}