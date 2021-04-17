using DentalSystem.Boundaries.UseCases.Common.Dto.Output;

namespace DentalSystem.Boundaries.UseCases.Common.Validation
{
    public interface IApplicationValidator<TModel>
    {
        IValidationResult Validate(TModel model);
    }
}