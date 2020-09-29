using System.Collections.Generic;
using DentalScheduler.Interfaces.Dto.Output.Common;

namespace DentalScheduler.Interfaces.UseCases.Common.Validation
{
    public interface IValidationError : IError
    {
        string PropertyName { get; set; }

        IList<string> Errors { get; set; }
    }
}