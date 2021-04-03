using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DentalSystem.Interfaces.Infrastructure.Common.Persistence
{
    public interface IInitialData
    {
        Type EntityType { get; }

        int Priority { get; }

        IEnumerable<object> GetData();

        Task<bool> InitData();
    }
}