using System;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto;

namespace DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input
{
    public interface IUpdateTreatmentSessionInput : ITreatmentSessionInput, IReference
    { }
}