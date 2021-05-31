using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence
{
    public interface IWriteRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        void Remove(TEntity entity);

        void Remove(IEnumerable<TEntity> entities);
    }
}