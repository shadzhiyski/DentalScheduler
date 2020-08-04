using System.Collections.Generic;
using DentalScheduler.Interfaces.Models.Output.Common;

namespace DentalScheduler.Interfaces.Models.Output
{
    public interface IAuthResult
    {
        bool Succeeded { get; }
        
        IEnumerable<IError> Errors { get; }
    }
}