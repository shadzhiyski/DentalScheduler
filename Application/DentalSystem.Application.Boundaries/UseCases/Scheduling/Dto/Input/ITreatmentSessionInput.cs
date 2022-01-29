using System;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto;

namespace DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input
{
    public interface ITreatmentSessionInput : IPeriod, ITreatmentSessionReferencesInput
    {
        string Status { get; }
    }
}