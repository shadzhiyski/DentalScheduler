using DentalSystem.Application.Boundaries.UseCases.Common.Dto;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DentalSystem.Application.UseCases.Common.Validation
{
    public class PeriodValidator : AbstractValidator<IPeriod>
    {
        public const string RequiredStartMessageName = "RequiredStart";
        public const string RequiredEndMessageName = "RequiredEnd";
        public const string InvalidPeriodMessageName = "InvalidPeriod";

        public PeriodValidator(IStringLocalizer<PeriodValidator> localizer)
        {
            RuleFor(model => model.Start)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(localizer[RequiredStartMessageName]);

            RuleFor(model => model.End)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(localizer[RequiredEndMessageName])
                .GreaterThan(model => model.Start)
                .WithMessage(localizer[InvalidPeriodMessageName]);
        }
    }
}