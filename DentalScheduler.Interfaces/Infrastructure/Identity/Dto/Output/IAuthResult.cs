using System.Collections.Generic;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.Interfaces.Infrastructure.Identity.Dto.Output
{
    public interface IAuthResult
    {
        bool Succeeded { get; }

        IEnumerable<IError> Errors { get; }
    }
}