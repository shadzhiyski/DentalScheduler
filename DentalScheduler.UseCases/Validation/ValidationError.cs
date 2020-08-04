using System.Collections.Generic;
using DentalScheduler.Interfaces.Models.Output.Common;
using DentalScheduler.Interfaces.UseCases.Validation;

namespace DentalScheduler.UseCases.Validation
{
    public class ValidationError : IValidationError
    {
        public ErrorType Type => ErrorType.Validation;

        public string Message { get; set; }

        public string PropertyName { get; set; }
        
        public object AttemptedValue { get; set; }
        
        public object CustomState { get; set; }
        
        public Severity Severity { get; set; }
        
        public string ErrorCode { get; set; }
        
        public object[] FormattedMessageArguments { get; set; }
        
        public Dictionary<string, object> FormattedMessagePlaceholderValues { get; set; }
        
        public string ResourceName { get; set; }
    }
}