using DentalScheduler.Interfaces.UseCases.Validation;
using FluentValidation.Results;
using Mapster;

namespace DentalScheduler.Config.Mappings
{
    public class ValidationErrorMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ValidationFailure, IValidationError>()
                .Map(dest => dest.Message, src => src.ErrorMessage);
        }
    }
}