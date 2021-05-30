using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence
{
    public interface IInitialData
    {
        Type EntityType { get; }

        int Priority { get; }

        IEnumerable<object> GetData();

        Task<bool> InitData(CancellationToken cancellationToken);
    }
}