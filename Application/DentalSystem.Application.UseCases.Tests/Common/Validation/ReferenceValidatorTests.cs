using System;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto;
using DentalSystem.Application.UseCases.Common.Validation;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Xunit;

namespace DentalSystem.Application.UseCases.Tests.Common.Validation
{
    record ReferenceDto() : IReference
    {
        public Guid? ReferenceId { get; init; }
    };

    public class ReferenceValidatorTests : BaseTests
    {
        [Fact]
        public void ValidInput_ShouldReturnValidResult()
        {
            // Arrange
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<ReferenceValidator>>();
            var validator = ServiceProvider.GetRequiredService<ReferenceValidator>();

            var validInput = new ReferenceDto { ReferenceId = Guid.NewGuid() };

            // Act
            var validationResult = validator.Validate(validInput);

            // Assert
            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void MissingReferenceId_ShouldReturnInvalidResult()
        {
            // Arrange
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<ReferenceValidator>>();
            var validator = ServiceProvider.GetRequiredService<ReferenceValidator>();

            var invalidInput = new ReferenceDto { ReferenceId = default };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(IReference.ReferenceId),
                message: localizer[ReferenceValidator.RequiredReferenceIdMessageName]
            );
        }

        [Fact]
        public void EmptyReferenceId_ShouldReturnInvalidResult()
        {
            // Arrange
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<ReferenceValidator>>();
            var validator = ServiceProvider.GetRequiredService<ReferenceValidator>();

            var invalidInput = new ReferenceDto { ReferenceId = Guid.Empty };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(IReference.ReferenceId),
                message: localizer[ReferenceValidator.EmptyReferenceIdMessageName]
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