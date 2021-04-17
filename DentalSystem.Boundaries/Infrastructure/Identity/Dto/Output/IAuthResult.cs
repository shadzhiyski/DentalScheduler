using System.Collections.Generic;
using DentalSystem.Boundaries.UseCases.Common.Dto.Output;

namespace DentalSystem.Boundaries.Infrastructure.Identity.Dto.Output
{
    public interface IAuthResult
    {
        bool Succeeded { get; }

        IEnumerable<IError> Errors { get; }
    }
}