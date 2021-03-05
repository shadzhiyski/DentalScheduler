using System.Linq;
using DentalScheduler.Interfaces.UseCases.Common.Validation;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;
using FluentValidation;
using Mapster;

namespace DentalScheduler.UseCases.Common.Validation
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
            var fluentValidationResult = FluentValidator.Validate(model);

            var validationResult = new ValidationResult();

            var errors = fluentValidationResult.Errors
                .GroupBy(ve => ve.PropertyName)
                .Select(veg => new ValidationError
                {
                    PropertyName = veg.Key,
                    Errors = veg.Select(ve => ve.ErrorMessage).ToList()
                })
                .Cast<IValidationError>()
                .ToList();

            validationResult.Errors = errors;

            return validationResult;
        }
    }
}