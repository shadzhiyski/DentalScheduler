using System.Collections.Generic;
using DentalScheduler.Interfaces.Models.Output;
using DentalScheduler.Interfaces.Models.Output.Common;

namespace DentalScheduler.DTO.Output 
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