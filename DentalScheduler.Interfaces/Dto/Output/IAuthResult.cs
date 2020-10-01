using System.Collections.Generic;
using DentalScheduler.Interfaces.Dto.Output.Common;

namespace DentalScheduler.Interfaces.Dto.Output
{
    public interface IAuthResult
    {
        bool Succeeded { get; }
        
        IEnumerable<IError> Errors { get; }
    }
}