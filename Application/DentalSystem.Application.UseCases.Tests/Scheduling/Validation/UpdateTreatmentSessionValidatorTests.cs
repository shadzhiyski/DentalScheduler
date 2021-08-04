using System;
using DentalSystem.Application.UseCases.Common.Validation;
using DentalSystem.Application.UseCases.Scheduling.Dto.Input;
using DentalSystem.Application.UseCases.Scheduling.Validation;
using DentalSystem.Application.UseCases.Tests.Common;
using DentalSystem.Domain.Scheduling.Enumerations;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Xunit;

namespace DentalSystem.Application.UseCases.Tests.Scheduling.Validation
{
    public class UpdateTreatmentSessionValidatorTests : BaseTests
    {
        public UpdateTreatmentSessionInput ValidInput => new UpdateTreatmentSessionInput()
            {
                ReferenceId = Guid.NewGuid(),
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
            var validator = ServiceProvider.GetRequiredService<UpdateTreatmentSessionValidator>();

            // Act
            var validationResult = validator.Validate(ValidInput);

            // Assert
            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void MissingReferenceId_ShouldReturnInvalidResult()
        {
            // Arrange
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<ReferenceValidator>>();
            var validator = ServiceProvider.GetRequiredService<UpdateTreatmentSessionValidator>();

            var invalidInput = ValidInput with { ReferenceId = default };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(UpdateTreatmentSessionInput.ReferenceId),
                message: localizer[ReferenceValidator.RequiredReferenceIdMessageName]
            );
        }

        [Fact]
        public void EmptyReferenceId_ShouldReturnInvalidResult()
        {
            // Arrange
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<ReferenceValidator>>();
            var validator = ServiceProvider.GetRequiredService<UpdateTreatmentSessionValidator>();

            var invalidInput = ValidInput with { ReferenceId = Guid.Empty };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(UpdateTreatmentSessionInput.ReferenceId),
                message: localizer[ReferenceValidator.EmptyReferenceIdMessageName]
            );
        }

        [Fact]
        public void MissingTreatmentReferenceId_ShouldReturnInvalidResult()
        {
            // Arrange
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionValidator>>();
            var validator = ServiceProvider.GetRequiredService<UpdateTreatmentSessionValidator>();

            var invalidInput = ValidInput
                with { TreatmentReferenceId = default };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(UpdateTreatmentSessionInput.TreatmentReferenceId),
                message: localizer[TreatmentSessionValidator.RequiredTreatmentMessageName]
            );
        }

        [Fact]
        public void MissingPatientReferenceId_ShouldReturnInvalidResult()
        {
            // Arrange
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionValidator>>();
            var validator = ServiceProvider.GetRequiredService<UpdateTreatmentSessionValidator>();

            var invalidInput = ValidInput
                with { PatientReferenceId = default };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(UpdateTreatmentSessionInput.PatientReferenceId),
                message: localizer[TreatmentSessionValidator.RequiredPatientMessageName]
            );
        }

        [Fact]
        public void MissingDentalTeamReferenceId_ShouldReturnInvalidResult()
        {
            // Arrange
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionValidator>>();
            var validator = ServiceProvider.GetRequiredService<UpdateTreatmentSessionValidator>();

            var invalidInput = ValidInput
                with { DentalTeamReferenceId = default };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(UpdateTreatmentSessionInput.DentalTeamReferenceId),
                message: localizer[TreatmentSessionValidator.RequiredDentalTeamMessageName]
            );
        }

        [Fact]
        public void MissingStart_ShouldReturnInvalidResult()
        {
            // Arrange
            var periodLocalizer = ServiceProvider.GetRequiredService<IStringLocalizer<UseCases.Common.Validation.PeriodValidator>>();
            var validator = ServiceProvider.GetRequiredService<UpdateTreatmentSessionValidator>();

            var invalidInput = ValidInput
                with { Start = default };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(UpdateTreatmentSessionInput.Start),
                message: periodLocalizer[UseCases.Common.Validation.PeriodValidator.RequiredStartMessageName]
            );
        }

        [Fact]
        public void MissingEnd_ShouldReturnInvalidResult()
        {
            // Arrange
            var periodLocalizer = ServiceProvider.GetRequiredService<IStringLocalizer<UseCases.Common.Validation.PeriodValidator>>();
            var validator = ServiceProvider.GetRequiredService<UpdateTreatmentSessionValidator>();

            var invalidInput = ValidInput
                with { End = default };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(UpdateTreatmentSessionInput.End),
                message: periodLocalizer[UseCases.Common.Validation.PeriodValidator.RequiredEndMessageName]
            );
        }

        [Fact]
        public void InvalidPeriod_ShouldReturnInvalidResult()
        {
            // Arrange
            var periodLocalizer = ServiceProvider.GetRequiredService<IStringLocalizer<UseCases.Common.Validation.PeriodValidator>>();
            var validator = ServiceProvider.GetRequiredService<UpdateTreatmentSessionValidator>();

            var invalidInput = ValidInput
                with { End = ValidInput.Start.Value.AddHours(-1) };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(UpdateTreatmentSessionInput.End),
                message: periodLocalizer[UseCases.Common.Validation.PeriodValidator.InvalidPeriodMessageName]
            );
        }

        [Fact]
        public void MaxDurationExceeded_ShouldReturnInvalidResult()
        {
            // Arrange
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionValidator>>();
            var validator = ServiceProvider.GetRequiredService<UpdateTreatmentSessionValidator>();

            var invalidInput = ValidInput
                with { End = ValidInput.Start.Value.AddHours(3) };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(UpdateTreatmentSessionInput.End),
                message: localizer[TreatmentSessionValidator.MaxDurationMessageName]
            );
        }

        [Fact]
        public void UnsupportedStatus_ShouldReturnInvalidResult()
        {
            // Arrange
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionValidator>>();
            var validator = ServiceProvider.GetRequiredService<UpdateTreatmentSessionValidator>();

            var invalidInput = ValidInput
                with { Status = "Invalid Status" };

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(UpdateTreatmentSessionInput.Status),
                message: localizer[TreatmentSessionValidator.UnsupportedStatusMessageName]
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