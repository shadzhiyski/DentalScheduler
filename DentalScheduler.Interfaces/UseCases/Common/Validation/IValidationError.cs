using System.Collections.Generic;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.Interfaces.UseCases.Common.Validation
{
    public interface IValidationError : IError
    {
        string PropertyName { get; set; }

        IList<string> Errors { get; set; }
    }
}