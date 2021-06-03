using System;

namespace DentalSystem.Application.Boundaries.UseCases.Common.Dto
{
    public interface IPeriod
    {
        DateTimeOffset? Start { get; }

        DateTimeOffset? End { get; }
    }
}