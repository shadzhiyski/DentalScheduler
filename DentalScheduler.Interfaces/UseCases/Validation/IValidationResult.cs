using System.Collections.Generic;

namespace DentalScheduler.Interfaces.UseCases.Validation
{
    public interface IValidationResult
    {
        bool IsValid { get; set; }
        
        IList<IValidationError> Errors { get; set; }
        
        string[] RuleSetsExecuted { get; set; }
    }
}