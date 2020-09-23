using System.Collections.Generic;
using DentalScheduler.Interfaces.UseCases.Common.Validation;

namespace DentalScheduler.UseCases.Common.Validation
{
    public class ValidationResult : IValidationResult
    {
        public bool IsValid { get; set; }

        public IList<IValidationError> Errors { get; set; }

        public string[] RuleSetsExecuted { get; set; }
    }
}