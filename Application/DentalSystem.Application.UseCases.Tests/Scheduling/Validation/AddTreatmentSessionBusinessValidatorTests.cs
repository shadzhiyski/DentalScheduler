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
    public class AddTreatmentSessionBusinessValidatorTests : BaseTests
    {
        public AddTreatmentSessionBusinessValidatorTests() : base()
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

            TestTreatment = new Treatment
            {
                ReferenceId = ValidInput.TreatmentReferenceId.Value,
                Name = "Test Treatment"
            };

            TestPatient = new Patient
            {
                ReferenceId = ValidInput.PatientReferenceId.Value,
            };
        }

        public TreatmentSessionInput ValidInput { get; init; }
        public DentalTeam TestDentalTeam { get; init; }
        public Treatment TestTreatment { get; init; }
        public Patient TestPatient { get; init; }

        [Fact]
        public void ValidInput_ShouldReturnValidResult()
        {
            // Arrange
            var (validator, localizer) = GetBusinessValidator(
                new List<TreatmentSession>(),
                new List<DentalTeam> { TestDentalTeam },
                new List<Treatment>() { TestTreatment },
                new List<Patient>() { TestPatient }
            );

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
                },
                new List<DentalTeam> { TestDentalTeam },
                new List<Treatment>() { TestTreatment },
                new List<Patient>() { TestPatient }
            );

            // Act
            var validationResult = validator.Validate(validInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(TreatmentSessionInput.PatientReferenceId),
                message: localizer[AddTreatmentSessionBusinessValidator.OverlappingTreatmentSessionForPatientMessageName]
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
                },
                new List<DentalTeam> { TestDentalTeam },
                new List<Treatment>() { TestTreatment },
                new List<Patient>() { TestPatient }
            );

            // Act
            var validationResult = validator.Validate(validInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(TreatmentSessionInput.DentalTeamReferenceId),
                message: localizer[AddTreatmentSessionBusinessValidator.OverlappingTreatmentSessionForDentalTeamMessageName]
            );
        }

        private (AddTreatmentSessionBusinessValidator, IStringLocalizer<AddTreatmentSessionBusinessValidator>) GetBusinessValidator(
            IEnumerable<TreatmentSession> presentData,
            IEnumerable<DentalTeam> dentalTeamsPresentData,
            IEnumerable<Treatment> treatmentPresentData,
            IEnumerable<Patient> patientPresentData)
        {
            var simpleValidator = ServiceProvider.GetRequiredService<TreatmentSessionValidator>();
            var mockedRepository = new Mock<IReadRepository<TreatmentSession>>();

            mockedRepository.Setup(gr => gr.AsNoTracking())
                .Returns(() => presentData.AsQueryable().BuildMock().Object);

            var dentalTeamMockedRepository = new Mock<IReadRepository<DentalTeam>>();
            dentalTeamMockedRepository.Setup(gr => gr.AsNoTracking())
                .Returns(() => dentalTeamsPresentData.AsQueryable().BuildMock().Object);

            var treatmentMockedRepository = new Mock<IReadRepository<Treatment>>();
            treatmentMockedRepository.Setup(gr => gr.AsNoTracking())
                .Returns(() => treatmentPresentData.AsQueryable().BuildMock().Object);

            var patientMockedRepository = new Mock<IReadRepository<Patient>>();
            patientMockedRepository.Setup(gr => gr.AsNoTracking())
                .Returns(() => patientPresentData.AsQueryable().BuildMock().Object);

            var treatmentSessionReferencesBusinessValidator = new TreatmentSessionReferencesBusinessValidator(
                ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionReferencesBusinessValidator>>(),
                dentalTeamMockedRepository.Object,
                treatmentMockedRepository.Object,
                patientMockedRepository.Object
            );

            var businessLocalizer = ServiceProvider.GetRequiredService<IStringLocalizer<AddTreatmentSessionBusinessValidator>>();
            var businessValidator = new AddTreatmentSessionBusinessValidator(
                businessLocalizer,
                simpleValidator,
                treatmentSessionReferencesBusinessValidator,
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