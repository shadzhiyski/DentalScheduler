using System.Collections.Generic;
using DentalSystem.Interfaces.UseCases.Common.Dto.Output;

namespace DentalSystem.UseCases.Common.Validation
{
    public class ValidationResult : IValidationResult
    {
        public bool IsValid { get; set; }

        public IList<IValidationError> Errors { get; set; }

        public string[] RuleSetsExecuted { get; set; }
    }
}