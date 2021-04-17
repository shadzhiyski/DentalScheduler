using System.Collections.Generic;

namespace DentalSystem.Boundaries.UseCases.Common.Dto.Output
{
    public interface IValidationResult
    {
        bool IsValid { get; set; }

        IList<IValidationError> Errors { get; set; }

        string[] RuleSetsExecuted { get; set; }
    }
}