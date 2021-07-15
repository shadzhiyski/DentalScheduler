using System.Threading;
using System.Threading.Tasks;

namespace DentalSystem.Application.Boundaries.Infrastructure.Common
{
    public interface IInitializer
    {
        Task Initialize(CancellationToken cancellationToken);
    }
}