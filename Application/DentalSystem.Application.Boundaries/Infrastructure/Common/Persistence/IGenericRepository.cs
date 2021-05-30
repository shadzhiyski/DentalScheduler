namespace DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence
{
    public interface IGenericRepository<TEntity> : IReadRepository<TEntity>, IWriteRepository<TEntity> where TEntity : class
    { }
}