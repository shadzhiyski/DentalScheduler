using System.Linq;
using DentalScheduler.Interfaces.UseCases.Validation;
using FluentValidation.Results;
using Mapster;

namespace DentalScheduler.Config.Mappings
{
    public class ValidationResultMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ValidationResult, IValidationResult>()
                .ConstructUsing(dest => new UseCases.Validation.ValidationResult())
                .Map(
                    dest => dest.Errors, 
                    src => src.Errors.Select(e => e.Adapt<IValidationError>(config)).ToList()
                );
        }
    }
}