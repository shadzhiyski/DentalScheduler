using System.Collections.Generic;
using DentalScheduler.Interfaces.Infrastructure.Identity.Dto.Output;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.Infrastructure.Identity.Dto.Output
{
    public class AuthResult : IAuthResult
    {
        public AuthResult (bool succeeded, IEnumerable<IError> errors)
        {
            Errors = errors;
            Succeeded = succeeded;
        }

        public bool Succeeded { get; }

        public IEnumerable<IError> Errors { get; }
    }
}