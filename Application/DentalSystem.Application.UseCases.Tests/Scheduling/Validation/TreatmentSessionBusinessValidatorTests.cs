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
using Moq;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace DentalSystem.Application.UseCases.Tests.Scheduling.Validation
{
    public class TreatmentSessionBusinessValidatorTests : BaseTests
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
            var (validator, localizer) = GetBusinessValidator(new List<TreatmentSession>());

            // Act
            var validationResult = validator.Validate(ValidInput);

            // Assert
            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void OverlappingTreatmentSessionForPatient_ShouldReturnInvalidResult()
        {
            // Arrange
            var validInput = ValidInput;
            var (validator, localizer) = GetBusinessValidator(new List<TreatmentSession>()
            {
                new TreatmentSession
                {
                    Id = Guid.NewGuid(),
                    Patient = new Patient
                    {
                        Id = Guid.NewGuid(),
                        ReferenceId = validInput.PatientReferenceId.Value
                    },
                    DentalTeam = new DentalTeam
                    {
                        Id = Guid.NewGuid(),
                        ReferenceId = Guid.NewGuid()
                    },
                    Start = validInput.Start.Value,
                    End = validInput.End.Value,
                    Status = Enum.Parse<TreatmentSessionStatus>(validInput.Status)
                }
            });

            // Act
            var validationResult = validator.Validate(validInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(TreatmentSessionInput.PatientReferenceId),
                message: localizer[TreatmentSessionBusinessValidator.OverlappingTreatmentSessionForPatientMessageName]
            );
        }

        [Fact]
        public void OverlappingTreatmentSessionForDentalTeam_ShouldReturnInvalidResult()
        {
            // Arrange
            var validInput = ValidInput;
            var (validator, localizer) = GetBusinessValidator(new List<TreatmentSession>()
            {
                new TreatmentSession
                {
                    Id = Guid.NewGuid(),
                    Patient = new Patient
                    {
                        Id = Guid.NewGuid(),
                        ReferenceId = Guid.NewGuid()
                    },
                    DentalTeam = new DentalTeam
                    {
                        Id = Guid.NewGuid(),
                        ReferenceId = validInput.DentalTeamReferenceId.Value
                    },
                    Start = validInput.Start.Value,
                    End = validInput.End.Value,
                    Status = Enum.Parse<TreatmentSessionStatus>(validInput.Status)
                }
            });

            // Act
            var validationResult = validator.Validate(validInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(TreatmentSessionInput.DentalTeamReferenceId),
                message: localizer[TreatmentSessionBusinessValidator.OverlappingTreatmentSessionForDentalTeamMessageName]
            );
        }

        private (TreatmentSessionBusinessValidator, IStringLocalizer<TreatmentSessionBusinessValidator>) GetBusinessValidator(
            IEnumerable<TreatmentSession> presentData)
        {
            var simpleValidator = ServiceProvider.GetRequiredService<TreatmentSessionValidator>();
            var mockedRepository = new Mock<IReadRepository<TreatmentSession>>();

            var filteredData = presentData;
            mockedRepository.Setup(gr => gr.Where(It.IsAny<Expression<Func<TreatmentSession, bool>>>()))
                .Callback<Expression<Func<TreatmentSession, bool>>>(
                    (filterExpression) => filteredData = presentData.Where(filterExpression.Compile())
                )
                .Returns(() => filteredData.AsQueryable());

            var businessLocalizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionBusinessValidator>>();
            var businessValidator = new TreatmentSessionBusinessValidator(
                businessLocalizer,
                simpleValidator,
                mockedRepository.Object
            );

            return (businessValidator, businessLocalizer);
        }

        private void AssertInvalidResult(
            ValidationResult validationResult,
            string propertyName,
            string message)
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