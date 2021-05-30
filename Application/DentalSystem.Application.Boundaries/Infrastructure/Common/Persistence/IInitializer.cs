using System.Threading;
using System.Threading.Tasks;

namespace DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence
{
    public interface IInitializer
    {
        Task Initialize(CancellationToken cancellationToken);
    }
}