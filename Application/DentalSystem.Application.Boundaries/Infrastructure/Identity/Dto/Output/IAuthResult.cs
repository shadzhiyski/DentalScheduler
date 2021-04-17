using System.Collections.Generic;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;

namespace DentalSystem.Application.Boundaries.Infrastructure.Identity.Dto.Output
{
    public interface IAuthResult
    {
        bool Succeeded { get; }

        IEnumerable<IError> Errors { get; }
    }
}