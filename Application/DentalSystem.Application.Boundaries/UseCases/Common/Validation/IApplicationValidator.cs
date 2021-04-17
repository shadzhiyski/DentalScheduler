using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;

namespace DentalSystem.Application.Boundaries.UseCases.Common.Validation
{
    public interface IApplicationValidator<TModel>
    {
        IValidationResult Validate(TModel model);
    }
}