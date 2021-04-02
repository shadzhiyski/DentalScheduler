using System.Collections.Generic;
using DentalSystem.Interfaces.UseCases.Common.Dto.Output;

namespace DentalSystem.Interfaces.Infrastructure.Identity.Dto.Output
{
    public interface IAuthResult
    {
        bool Succeeded { get; }

        IEnumerable<IError> Errors { get; }
    }
}