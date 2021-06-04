using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input;
using FluentValidation;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto;

namespace DentalSystem.Application.UseCases.Scheduling.Validation
{
    public class UpdateTreatmentSessionValidator : AbstractValidator<IUpdateTreatmentSessionInput>
    {
        public UpdateTreatmentSessionValidator(
            TreatmentSessionValidator treatmentSessionValidator,
            AbstractValidator<IReference> referenceValidator)
        {
            RuleFor(m => m)
                .SetValidator(referenceValidator)
                .SetValidator(treatmentSessionValidator);
        }
    }
}