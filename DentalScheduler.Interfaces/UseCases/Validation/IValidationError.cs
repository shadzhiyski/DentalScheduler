using System.Collections.Generic;
using DentalScheduler.Interfaces.Models.Output.Common;

namespace DentalScheduler.Interfaces.UseCases.Validation
{
    public interface IValidationError : IError
    {
        string PropertyName { get; set; }

        IEnumerable<string> Messages { get; set; }
    }
}