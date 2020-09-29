using System.Collections.Generic;
using DentalScheduler.Interfaces.Dto.Output;
using DentalScheduler.Interfaces.Dto.Output.Common;

namespace DentalScheduler.Dto.Output 
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