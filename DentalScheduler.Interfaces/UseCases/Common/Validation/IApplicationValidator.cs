using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.Interfaces.UseCases.Common.Validation
{
    public interface IApplicationValidator<TModel>
    {
        IValidationResult Validate(TModel model);
    }
}