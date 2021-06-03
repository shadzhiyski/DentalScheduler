using System;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto;
using DentalSystem.Application.UseCases.Common.Validation;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Xunit;

namespace DentalSystem.Application.UseCases.Tests.Common.Validation
{
    record PeriodDto() : IPeriod
    {
        public DateTimeOffset? Start { get; init; }

        public DateTimeOffset? End { get; init; }
    }

    public class PeriodValidatorTests : BaseTests
    {
        PeriodDto ValidInput => new PeriodDto()
            {
                Start = DateTimeOffset.Now,
                End = DateTimeOffset.Now.AddHours(1)
            };

        [Fact]
        public void ValidInput_ShouldReturnValidResult()
        {
            // Arrange
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<PeriodValidator>>();
            var validator = ServiceProvider.GetRequiredService<PeriodValidator>();

            // Act
            var validationResult = validator.Validate(ValidInput);

            // Assert
            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void MissingStart_ShouldReturnInvalidResult()
        {
            // Arrange
            var periodLocalizer = ServiceProvider.GetRequiredService<IStringLocalizer<PeriodValidator>>();
            var validator = ServiceProvider.GetRequiredService<PeriodValidator>();

            var invalidInput = ValidInput
                with { Start = default };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(IPeriod.Start),
                message: periodLocalizer[UseCases.Common.Validation.PeriodValidator.RequiredStartMessageName]
            );
        }

        [Fact]
        public void MissingEnd_ShouldReturnInvalidResult()
        {
            // Arrange
            var periodLocalizer = ServiceProvider.GetRequiredService<IStringLocalizer<PeriodValidator>>();
            var validator = ServiceProvider.GetRequiredService<PeriodValidator>();

            var invalidInput = ValidInput
                with { End = default };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(IPeriod.End),
                message: periodLocalizer[UseCases.Common.Validation.PeriodValidator.RequiredEndMessageName]
            );
        }

        [Fact]
        public void InvalidPeriod_ShouldReturnInvalidResult()
        {
            // Arrange
            var periodLocalizer = ServiceProvider.GetRequiredService<IStringLocalizer<PeriodValidator>>();
            var validator = ServiceProvider.GetRequiredService<PeriodValidator>();

            var invalidInput = ValidInput
                with { End = ValidInput.Start.Value.AddHours(-1) };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(IPeriod.End),
                message: periodLocalizer[UseCases.Common.Validation.PeriodValidator.InvalidPeriodMessageName]
            );
        }

        private void AssertInvalidResult(
            FluentValidation.Results.ValidationResult validationResult,
            string propertyName,
            string message = null)
        {
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors
                .Should()
                .ContainSingle(
                    error => error.PropertyName.Equals(propertyName)
                        && error.ErrorMessage.Equals(message)
                );
        }
    }
}