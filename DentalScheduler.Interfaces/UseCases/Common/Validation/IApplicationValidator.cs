using DentalScheduler.Interfaces.Models.Output.Common;

namespace DentalScheduler.Interfaces.UseCases.Common.Validation
{
    public interface IApplicationValidator<TModel>
    {
        IValidationResult Validate(TModel model);
    }
}