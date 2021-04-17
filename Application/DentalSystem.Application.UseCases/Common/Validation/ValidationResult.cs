using System.Collections.Generic;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;

namespace DentalSystem.Application.UseCases.Common.Validation
{
    public class ValidationResult : IValidationResult
    {
        public bool IsValid { get; set; }

        public IList<IValidationError> Errors { get; set; }

        public string[] RuleSetsExecuted { get; set; }
    }
}