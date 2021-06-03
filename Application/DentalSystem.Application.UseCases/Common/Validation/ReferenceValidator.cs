using System;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DentalSystem.Application.UseCases.Common.Validation
{
    public class ReferenceValidator : AbstractValidator<IReference>
    {
        public const string RequiredReferenceIdMessageName = "RequiredReferenceId";
        public const string EmptyReferenceIdMessageName = "EmptyReferenceId";

        public ReferenceValidator(IStringLocalizer<ReferenceValidator> localizer)
        {
            RuleFor(model => model.ReferenceId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(localizer[RequiredReferenceIdMessageName])
                .NotEqual(Guid.Empty)
                .WithMessage(localizer[EmptyReferenceIdMessageName]);
        }
    }
}