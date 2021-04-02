using DentalSystem.Interfaces.UseCases.Common.Dto.Output;

namespace DentalSystem.Interfaces.UseCases.Common.Validation
{
    public interface IApplicationValidator<TModel>
    {
        IValidationResult Validate(TModel model);
    }
}