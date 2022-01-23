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
    public class UpdateTreatmentSessionBusinessValidatorTests : BaseTests
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
            var validInput = ValidInput;
            var validator = GetBusinessValidator(new List<TreatmentSession>()
            {
                new TreatmentSession
                {
                    Id = Guid.NewGuid(),
                    ReferenceId = validInput.ReferenceId.Value,
                    Patient = new Patient
                    {
                        Id = Guid.NewGuid(),
                        ReferenceId = Guid.NewGuid()
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
            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void MissingTreatmentSessionForPatient_ShouldReturnInvalidResult()
        {
            // Arrange
            var validInput = ValidInput;
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<UpdateTreatmentSessionBusinessValidator>>();
            var validator = GetBusinessValidator(new List<TreatmentSession>());

            // Act
            var validationResult = validator.Validate(validInput);

            // Assert
            AssertInvalidResult(
                validationResult: validationResult,
                propertyName: nameof(UpdateTreatmentSessionInput.ReferenceId),
                message: localizer[UpdateTreatmentSessionBusinessValidator.NotExistingTreatmentSessionMessageName]
            );
        }

        [Fact]
        public void OverlappingTreatmentSessionForPatient_ShouldReturnInvalidResult()
        {
            // Arrange
            var validInput = ValidInput;
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionBusinessValidator>>();
            var validator = GetBusinessValidator(new List<TreatmentSession>()
            {
                new TreatmentSession
                {
                    Id = Guid.NewGuid(),
                    ReferenceId = validInput.ReferenceId.Value,
                    Patient = new Patient
                    {
                        Id = Guid.NewGuid(),
                        ReferenceId = validInput.PatientReferenceId.Value
                    },
                    DentalTeam = new DentalTeam
                    {
                        Id = Guid.NewGuid(),
                        ReferenceId = validInput.DentalTeamReferenceId.Value
                    },
                    Start = validInput.Start.Value,
                    End = validInput.End.Value,
                    Status = Enum.Parse<TreatmentSessionStatus>(validInput.Status)
                },
                new TreatmentSession
                {
                    Id = Guid.NewGuid(),
                    ReferenceId = Guid.NewGuid(),
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
                propertyName: nameof(UpdateTreatmentSessionInput.PatientReferenceId),
                message: localizer[TreatmentSessionBusinessValidator.OverlappingTreatmentSessionForPatientMessageName]
            );
        }

        [Fact]
        public void OverlappingSameTreatmentSessionForPatient_ShouldReturnValidResult()
        {
            // Arrange
            var validInput = ValidInput;
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionBusinessValidator>>();
            var validator = GetBusinessValidator(new List<TreatmentSession>()
            {
                new TreatmentSession
                {
                    Id = Guid.NewGuid(),
                    ReferenceId = validInput.ReferenceId.Value,
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
            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void OverlappingTreatmentSessionForDentalTeam_ShouldReturnInvalidResult()
        {
            // Arrange
            var validInput = ValidInput;
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionBusinessValidator>>();
            var validator = GetBusinessValidator(new List<TreatmentSession>()
            {
                new TreatmentSession
                {
                    Id = Guid.NewGuid(),
                    ReferenceId = validInput.ReferenceId.Value,
                    Patient = new Patient
                    {
                        Id = Guid.NewGuid(),
                        ReferenceId = validInput.PatientReferenceId.Value
                    },
                    DentalTeam = new DentalTeam
                    {
                        Id = Guid.NewGuid(),
                        ReferenceId = validInput.DentalTeamReferenceId.Value
                    },
                    Start = validInput.Start.Value,
                    End = validInput.End.Value,
                    Status = Enum.Parse<TreatmentSessionStatus>(validInput.Status)
                },
                new TreatmentSession
                {
                    Id = Guid.NewGuid(),
                    ReferenceId = Guid.NewGuid(),
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
                propertyName: nameof(UpdateTreatmentSessionInput.DentalTeamReferenceId),
                message: localizer[TreatmentSessionBusinessValidator.OverlappingTreatmentSessionForDentalTeamMessageName]
            );
        }

        [Fact]
        public void OverlappingSameTreatmentSessionForDentalTeam_ShouldReturnValidResult()
        {
            // Arrange
            var validInput = ValidInput;
            var localizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionBusinessValidator>>();
            var validator = GetBusinessValidator(new List<TreatmentSession>()
            {
                new TreatmentSession
                {
                    Id = Guid.NewGuid(),
                    ReferenceId = validInput.ReferenceId.Value,
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
            validationResult.IsValid.Should().BeTrue();
        }

        private UpdateTreatmentSessionBusinessValidator GetBusinessValidator(
            IEnumerable<TreatmentSession> presentData)
        {
            var simpleValidator = ServiceProvider.GetRequiredService<UpdateTreatmentSessionValidator>();
            var mockedRepository = new Mock<IReadRepository<TreatmentSession>>();

            var filteredData = presentData;
            mockedRepository.Setup(gr => gr.AsNoTracking())
                .Returns(() => filteredData.AsQueryable());

            mockedRepository.Setup(gr => gr.Where(It.IsAny<Expression<Func<TreatmentSession, bool>>>()))
                .Callback<Expression<Func<TreatmentSession, bool>>>(
                    (filterExpression) => filteredData = presentData.Where(filterExpression.Compile())
                )
                .Returns(() => filteredData.AsQueryable().BuildMock().Object);

            var addBusinessLocalizer = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionBusinessValidator>>();
            var businessLocalizer = ServiceProvider.GetRequiredService<IStringLocalizer<UpdateTreatmentSessionBusinessValidator>>();
            var businessValidator = new UpdateTreatmentSessionBusinessValidator(
                businessLocalizer,
                addBusinessLocalizer,
                simpleValidator,
                mockedRepository.Object
            );

            return businessValidator;
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