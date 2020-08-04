using System.Collections.Generic;
using DentalScheduler.Interfaces.Models.Output.Common;

namespace DentalScheduler.Interfaces.UseCases.Validation
{
    public interface IValidationError : IError
    {
        string PropertyName { get; set; }
        
        // string ErrorMessage { get; set; }
        
        object AttemptedValue { get; set; }
        
        object CustomState { get; set; }
        
        Severity Severity { get; set; }
        
        string ErrorCode { get; set; }
        
        object[] FormattedMessageArguments { get; set; }
        
        Dictionary<string, object> FormattedMessagePlaceholderValues { get; set; }
        
        string ResourceName { get; set; }
    }
}