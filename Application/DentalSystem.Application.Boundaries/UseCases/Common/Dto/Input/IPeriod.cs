using System;

namespace DentalSystem.Application.Boundaries.UseCases.Common.Dto.Input
{
    public interface IPeriod
    {
        DateTimeOffset? Start { get; }

        DateTimeOffset? End { get; }
    }
}