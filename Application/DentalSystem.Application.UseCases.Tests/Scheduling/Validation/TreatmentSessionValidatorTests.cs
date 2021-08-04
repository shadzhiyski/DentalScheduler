using System;
using DentalSystem.Domain.Scheduling;
using DentalSystem.Application.UseCases.Scheduling.Dto.Input;
using DentalSystem.Application.UseCases.Scheduling.Validation;
using DentalSystem.Application.UseCases.Tests.Common;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Xunit;

namespace DentalSystem.Application.UseCases.Tests.Scheduling.Validation
{
    public class TreatmentSessionValidatorTests : BaseTests
    {
        public TreatmentSessionInput ValidInput => new TreatmentSessionInput()
            {
                TreatmentReferenceId = Guid.NewGuid(),
                PatientReferenceId = Guid.NewGuid(),
                DentalTeamReferenceId = Guid.NewGuid(),
                Status = TreatmentSessionStatus.Requested.ToString(),
                Start = DateTimeOffset.UtcNow,
                End = DateTimeOffset.UtcNow.AddMinutes(30)
            };

        [Fact]
        public void ValidInput_ShouldReturnValidResult()
        {
            // Arrange
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionValidator>>();
            var validator = ServiceProvider.GetRequiredService<TreatmentSessionValidator>();

            // Act
            var validationResult = validator.Validate(ValidInput);

            // Assert
            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void MissingTreatmentReferenceId_ShouldReturnInvalidResult()
        {
            // Arrange
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionValidator>>();
            var validator = ServiceProvider.GetRequiredService<TreatmentSessionValidator>();

            var invalidInput = ValidInput
                with { TreatmentReferenceId = default };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(TreatmentSessionInput.TreatmentReferenceId),
                message: localizer[TreatmentSessionValidator.RequiredTreatmentMessageName]
            );
        }

        [Fact]
        public void MissingPatientReferenceId_ShouldReturnInvalidResult()
        {
            // Arrange
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionValidator>>();
            var validator = ServiceProvider.GetRequiredService<TreatmentSessionValidator>();

            var invalidInput = ValidInput
                with { PatientReferenceId = default };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(TreatmentSessionInput.PatientReferenceId),
                message: localizer[TreatmentSessionValidator.RequiredPatientMessageName]
            );
        }

        [Fact]
        public void MissingDentalTeamReferenceId_ShouldReturnInvalidResult()
        {
            // Arrange
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionValidator>>();
            var validator = ServiceProvider.GetRequiredService<TreatmentSessionValidator>();

            var invalidInput = ValidInput
                with { DentalTeamReferenceId = default };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(TreatmentSessionInput.DentalTeamReferenceId),
                message: localizer[TreatmentSessionValidator.RequiredDentalTeamMessageName]
            );
        }

        [Fact]
        public void MissingStart_ShouldReturnInvalidResult()
        {
            // Arrange
            var periodLocalizer = ServiceProvider.GetRequiredService<IStringLocalizer<UseCases.Common.Validation.PeriodValidator>>();
            var validator = ServiceProvider.GetRequiredService<TreatmentSessionValidator>();

            var invalidInput = ValidInput
                with { Start = default };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(TreatmentSessionInput.Start),
                message: periodLocalizer[UseCases.Common.Validation.PeriodValidator.RequiredStartMessageName]
            );
        }

        [Fact]
        public void MissingEnd_ShouldReturnInvalidResult()
        {
            // Arrange
            var periodLocalizer = ServiceProvider.GetRequiredService<IStringLocalizer<UseCases.Common.Validation.PeriodValidator>>();
            var validator = ServiceProvider.GetRequiredService<TreatmentSessionValidator>();

            var invalidInput = ValidInput
                with { End = default };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(TreatmentSessionInput.End),
                message: periodLocalizer[UseCases.Common.Validation.PeriodValidator.RequiredEndMessageName]
            );
        }

        [Fact]
        public void InvalidPeriod_ShouldReturnInvalidResult()
        {
            // Arrange
            var periodLocalizer = ServiceProvider.GetRequiredService<IStringLocalizer<UseCases.Common.Validation.PeriodValidator>>();
            var validator = ServiceProvider.GetRequiredService<TreatmentSessionValidator>();

            var invalidInput = ValidInput
                with { End = ValidInput.Start.Value.AddHours(-1) };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(TreatmentSessionInput.End),
                message: periodLocalizer[UseCases.Common.Validation.PeriodValidator.InvalidPeriodMessageName]
            );
        }

        [Fact]
        public void MaxDurationExceeded_ShouldReturnInvalidResult()
        {
            // Arrange
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionValidator>>();
            var validator = ServiceProvider.GetRequiredService<TreatmentSessionValidator>();

            var invalidInput = ValidInput
                with { End = ValidInput.Start.Value.AddHours(3) };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(TreatmentSessionInput.End),
                message: localizer[TreatmentSessionValidator.MaxDurationMessageName]
            );
        }

        [Fact]
        public void UnsupportedStatus_ShouldReturnInvalidResult()
        {
            // Arrange
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionValidator>>();
            var validator = ServiceProvider.GetRequiredService<TreatmentSessionValidator>();

            var invalidInput = ValidInput
                with { Status = "Invalid Status" };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(TreatmentSessionInput.Status),
                message: localizer[TreatmentSessionValidator.UnsupportedStatusMessageName]
            );
        }

        private void AssertInvalidResult(
            ValidationResult validationResult,
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