using System;
using DentalSystem.Entities;
using DentalSystem.UseCases.Scheduling.Dto.Input;
using DentalSystem.UseCases.Scheduling.Validation;
using FluentAssertions;
using FluentValidation.Results;
using Xunit;

namespace DentalSystem.UseCases.Tests.Scheduling.Validation
{
    public class TreatmentSessionValidatorTests
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
            var validator = new TreatmentSessionValidator();

            // Act
            var validationResult = validator.Validate(ValidInput);

            // Assert
            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void MissingTreatmentReferenceId_ShouldReturnInvalidResult()
        {
            // Arrange
            var validator = new TreatmentSessionValidator();

            var invalidInput = ValidInput;
            invalidInput.TreatmentReferenceId = default;

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult,
                nameof(TreatmentSessionInput.TreatmentReferenceId)
            );
        }

        [Fact]
        public void MissingPatientReferenceId_ShouldReturnInvalidResult()
        {
            // Arrange
            var validator = new TreatmentSessionValidator();

            var invalidInput = ValidInput;
            invalidInput.PatientReferenceId = default;

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult,
                nameof(TreatmentSessionInput.PatientReferenceId)
            );
        }

        [Fact]
        public void MissingDentalTeamReferenceId_ShouldReturnInvalidResult()
        {
            // Arrange
            var validator = new TreatmentSessionValidator();

            var invalidInput = ValidInput;
            invalidInput.DentalTeamReferenceId = default;

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult,
                nameof(TreatmentSessionInput.DentalTeamReferenceId)
            );
        }

        [Fact]
        public void MissingStart_ShouldReturnInvalidResult()
        {
            // Arrange
            var validator = new TreatmentSessionValidator();

            var invalidInput = ValidInput;
            invalidInput.Start = default;

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult,
                nameof(TreatmentSessionInput.Start)
            );
        }

        [Fact]
        public void MissingEnd_ShouldReturnInvalidResult()
        {
            // Arrange
            var validator = new TreatmentSessionValidator();

            var invalidInput = ValidInput;
            invalidInput.End = default;

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult,
                nameof(TreatmentSessionInput.End)
            );
        }

        [Fact]
        public void InvalidPeriod_ShouldReturnInvalidResult()
        {
            // Arrange
            var validator = new TreatmentSessionValidator();

            var invalidInput = ValidInput;
            invalidInput.End = invalidInput.Start.Value.AddHours(-1);

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult,
                nameof(TreatmentSessionInput.End)
            );
        }

        [Fact]
        public void InvalidStatus_ShouldReturnInvalidResult()
        {
            // Arrange
            var validator = new TreatmentSessionValidator();

            var invalidInput = ValidInput;
            invalidInput.Status = "Invalid Status";

            // Act
            var validationResult = validator.Validate(invalidInput);

            // Assert
            AssertInvalidResult(
                validationResult,
                nameof(TreatmentSessionInput.Status)
            );
        }

        private void AssertInvalidResult(
            ValidationResult validationResult,
            string propertyName)
        {
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors
                .Should()
                .ContainSingle(
                    error => error.PropertyName.Equals(propertyName)
                );
        }
    }
}