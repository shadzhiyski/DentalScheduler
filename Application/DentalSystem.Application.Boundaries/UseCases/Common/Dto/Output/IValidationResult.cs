using System.Collections.Generic;

namespace DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output
{
    public interface IValidationResult
    {
        bool IsValid { get; }

        IList<IValidationError> Errors { get; }

        string[] RuleSetsExecuted { get; }
    }
}