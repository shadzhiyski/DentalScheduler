using DentalScheduler.Interfaces.UseCases.Validation;
using FluentValidation;
using Mapster;

namespace DentalScheduler.UseCases.Validation
{
    public class ApplicationValidator<TModel> : IApplicationValidator<TModel>
    {
        public TypeAdapterConfig MappingConfig { get; }

        public IValidator<TModel> FluentValidator { get; }
        
        public ApplicationValidator(TypeAdapterConfig mappingConfig, AbstractValidator<TModel> fluentValidator)
        {
            MappingConfig = mappingConfig;
            FluentValidator = fluentValidator;
        }

        public IValidationResult Validate(TModel model)
        {
            return FluentValidator.Validate(model)
                .Adapt<IValidationResult>(MappingConfig);
        }
    }
}