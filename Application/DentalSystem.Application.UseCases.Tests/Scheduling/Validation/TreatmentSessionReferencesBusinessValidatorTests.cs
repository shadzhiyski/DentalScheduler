using System;
using DentalSystem.Domain.Scheduling.Entities;
using DentalSystem.Domain.Scheduling.Enumerations;
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
using MockQueryable.Moq;

namespace DentalSystem.Application.UseCases.Tests.Scheduling.Validation
{
    public class TreatmentSessionReferencesBusinessValidatorTests : BaseTests
    {
        public TreatmentSessionReferencesBusinessValidatorTests() : base()
        {
            ValidInput = new TreatmentSessionInput()
            {
                TreatmentReferenceId = Guid.NewGuid(),
                PatientReferenceId = Guid.NewGuid(),
                DentalTeamReferenceId = Guid.NewGuid(),
                Status = TreatmentSessionStatus.Requested.ToString(),
                Start = DateTimeOffset.UtcNow,
                End = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            TestDentalTeam = new DentalTeam
            {
                Id = Guid.NewGuid(),
                Name = "Test Dental Team",
                ReferenceId = ValidInput.DentalTeamReferenceId.Value,
            };
        }

        public TreatmentSessionInput ValidInput { get; init; }
        public DentalTeam TestDentalTeam { get; init; }

        [Fact]
        public void ValidInput_ShouldReturnValidResult()
        {
            // Arrange
            var (validator, localizer) = GetBusinessValidator(
                new List<TreatmentSession>(),
                new List<DentalTeam> { TestDentalTeam }
            );

            // Act
            var validationResult = validator.Validate(ValidInput);

            // Assert
            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void NotExistingDentalTeam_ShouldReturnInvalidResult()
        {
            // Arrange
            var notExistingDentalTeamReferenceId = Guid.Empty;
            var validInput = ValidInput
                with { DentalTeamReferenceId = notExistingDentalTeamReferenceId };
            var (validator, localizer) = GetBusinessValidator(
                new List<TreatmentSession>() { },
                new List<DentalTeam>()
            );

            // Act
            var validationResult = validator.Validate(validInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(TreatmentSessionInput.DentalTeamReferenceId),
                message: localizer[TreatmentSessionReferencesBusinessValidator.InvalidDentalTeamMessageName]
            );
        }

        private (TreatmentSessionReferencesBusinessValidator, IStringLocalizer<TreatmentSessionReferencesBusinessValidator>) GetBusinessValidator(
            IEnumerable<TreatmentSession> presentData,
            IEnumerable<DentalTeam> dentalTeamsPresentData = default)
        {
            var dentalTeamMockedRepository = new Mock<IReadRepository<DentalTeam>>();
            var dentalTeamsFilteredData = dentalTeamsPresentData;
            dentalTeamMockedRepository.Setup(gr => gr.Where(It.IsAny<Expression<Func<DentalTeam, bool>>>()))
                .Callback<Expression<Func<DentalTeam, bool>>>(
                    (filterExpression) => dentalTeamsFilteredData = dentalTeamsPresentData.Where(filterExpression.Compile())
                )
                .Returns(() => dentalTeamsFilteredData.AsQueryable().BuildMock().Object);

            var businessLocalizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionReferencesBusinessValidator>>();
            var treatmentSessionReferencesBusinessValidator = new TreatmentSessionReferencesBusinessValidator(
                businessLocalizer,
                dentalTeamMockedRepository.Object
            );

            return (treatmentSessionReferencesBusinessValidator, businessLocalizer);
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