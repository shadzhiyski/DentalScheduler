using System.Collections.Generic;

namespace DentalSystem.Interfaces.UseCases.Common.Dto.Output
{
    public interface IValidationResult
    {
        bool IsValid { get; set; }

        IList<IValidationError> Errors { get; set; }

        string[] RuleSetsExecuted { get; set; }
    }
}