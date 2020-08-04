using System;
using DentalScheduler.Interfaces.Models.Output.Common;
using DentalScheduler.Interfaces.UseCases.Validation;
using FluentValidation;
using Mapster;

namespace DentalScheduler.UseCases.Validation
{
    public class ApplicationValidator<TModel> : IApplicationValidator<TModel>
    {
        public IValidator<TModel> FluentValidator { get; }
        
        public ApplicationValidator(AbstractValidator<TModel> fluentValidator)
        {
            FluentValidator = fluentValidator;
        }

        public IValidationResult Validate(TModel model)
        {
            var result = FluentValidator.Validate(model);
            return result.Adapt<IValidationResult>();
        }
    }
}