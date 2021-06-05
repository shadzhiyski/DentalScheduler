using System;
using System.Linq;
using DentalSystem.Application.UseCases.Scheduling.Dto.Input;
using DentalSystem.Application.UseCases.Tests.Common;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using DentalSystem.Entities.Identity;
using DentalSystem.Application.Boundaries.Infrastructure.Identity;
using Microsoft.Extensions.Localization;
using DentalSystem.Application.UseCases.Scheduling.Validation;
using System.Collections.Generic;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;
using DentalSystem.Application.UseCases.Common.Validation;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace DentalSystem.Application.UseCases.Tests.Scheduling
{
    public class UpdateTreatmentSessionCommandIntegrationTests : BaseIntegrationTests
    {
        public UpdateTreatmentSessionCommandIntegrationTests() : base(new ServiceCollection())
        {
            Sut = ServiceProvider.GetRequiredService<IMediator>();

            Patient = CreatePatientUser();

            DentalTeam = ServiceProvider.GetRequiredService<IReadRepository<DentalTeam>>()
                .AsNoTracking()
                .SingleOrDefault(dt => dt.Name == "DentalTeam 01");

            var start = DateTimeOffset.Now;
            TreatmentSession = CreateTreatmentSession(
                patientId: Patient.Id,
                dentalTeamId: DentalTeam.Id,
                treatmentId: Treatments.First().Id,
                start: start,
                end: start.AddHours(1),
                status: TreatmentSessionStatus.Requested
            );
        }

        public Patient Patient { get; }

        public DentalTeam DentalTeam { get; }

        public TreatmentSession TreatmentSession { get; }

        public IMediator Sut { get; }

        [Fact]
        public async void UpdateTreatmentSession_ValidInput_ShouldReturnSuccessfullMessage()
        {
            // Arrange
            var input = new UpdateTreatmentSessionInput()
                {
                    ReferenceId = TreatmentSession.ReferenceId,
                    TreatmentReferenceId = TreatmentSession.Treatment.ReferenceId,
                    PatientReferenceId = TreatmentSession.Patient.ReferenceId,
                    DentalTeamReferenceId = TreatmentSession.DentalTeam.ReferenceId,
                    Start = TreatmentSession.Start.AddHours(1),
                    End = TreatmentSession.End.AddHours(1),
                    Status = TreatmentSession.Status.ToString()
                };

            // Act
            var result = await Sut.Send(input, default);
            var messageOutput = result.Value;

            // Assert
            messageOutput.Message.Should().BeEquivalentTo("Treatment Session successfully updated.");
        }

        [Fact]
        public async void UpdateTreatmentSession_InvalidInput_ShouldReturnErrorResult()
        {
            // Arrange
            var input = new UpdateTreatmentSessionInput();

            // Act
            var result = await Sut.Send(input, default);

            // Assert
            var allValidationMessages = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionValidator>>()
                .GetAllStrings()
                .Union(ServiceProvider.GetRequiredService<IStringLocalizer<PeriodValidator>>().GetAllStrings())
                .Union(ServiceProvider.GetRequiredService<IStringLocalizer<ReferenceValidator>>().GetAllStrings())
                .Select(ls => ls.Value);
            var allUniqueValidationMessages = new HashSet<string>(allValidationMessages);
            result.Errors
                .Cast<IValidationError>()
                .SelectMany(ve => ve.Errors)
                .Should()
                .OnlyContain(errMsg => allUniqueValidationMessages.Contains(errMsg));
        }

        [Fact]
        public async void UpdateTreatmentSession_ValidInputViolatingOverlappingBusinessRules_ShouldReturnErrorResult()
        {
            // Arrange
            var input = new UpdateTreatmentSessionInput()
                {
                    ReferenceId = TreatmentSession.ReferenceId,
                    TreatmentReferenceId = TreatmentSession.Treatment.ReferenceId,
                    PatientReferenceId = TreatmentSession.Patient.ReferenceId,
                    DentalTeamReferenceId = TreatmentSession.DentalTeam.ReferenceId,
                    Start = TreatmentSession.Start.AddHours(1),
                    End = TreatmentSession.End.AddHours(1),
                    Status = TreatmentSession.Status.ToString()
                };
            var overlappingTreatmentSession = CreateTreatmentSession(
                patientId: Patient.Id,
                dentalTeamId: DentalTeam.Id,
                treatmentId: Treatments.First().Id,
                start: input.Start.Value,
                end: input.End.Value,
                status: TreatmentSessionStatus.Requested
            );

            // Act
            var result = await Sut.Send(input, default);

            // Assert
            var allValidationMessages = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionBusinessValidator>>()
                .GetAllStrings()
                .Select(ls => ls.Value);
            var allUniqueValidationMessages = new HashSet<string>(allValidationMessages);
            result.Errors
                .Cast<IValidationError>()
                .SelectMany(ve => ve.Errors)
                .Should()
                .OnlyContain(errMsg => allUniqueValidationMessages.Contains(errMsg));
        }

        [Fact]
        public async void UpdateTreatmentSession_MissingTreatmentSession_ShouldReturnErrorResult()
        {
            // Arrange
            var input = new UpdateTreatmentSessionInput()
                {
                    ReferenceId = Guid.NewGuid(),
                    TreatmentReferenceId = TreatmentSession.Treatment.ReferenceId,
                    PatientReferenceId = TreatmentSession.Patient.ReferenceId,
                    DentalTeamReferenceId = TreatmentSession.DentalTeam.ReferenceId,
                    Start = TreatmentSession.Start.AddHours(2),
                    End = TreatmentSession.End.AddHours(2),
                    Status = TreatmentSession.Status.ToString()
                };

            // Act
            var result = await Sut.Send(input, default);

            // Assert
            var allValidationMessages = ServiceProvider.GetRequiredService<IStringLocalizer<UpdateTreatmentSessionBusinessValidator>>()
                .GetAllStrings()
                .Select(ls => ls.Value);
            var allUniqueValidationMessages = new HashSet<string>(allValidationMessages);
            result.Errors
                .Cast<IValidationError>()
                .SelectMany(ve => ve.Errors)
                .Should()
                .OnlyContain(errMsg => allUniqueValidationMessages.Contains(errMsg));
        }

        private Patient CreatePatientUser()
        {
            var patientUser = new User
            {
                Id = "19d65812-c648-419a-a5db-4c1c4b5d8a8e",
                Email = "patient@mail.com",
                UserName = "patient@mail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = "Patient",
                LastName = "Test"
            };

            var userService = ServiceProvider.GetRequiredService<IUserService<User>>();
            userService.CreateAsync(patientUser, "patient#123", default)
                .GetAwaiter()
                .GetResult();
            userService.AddToRoleAsync(patientUser, "Patient", default)
                .GetAwaiter()
                .GetResult();

            var patient = new Patient
            {
                IdentityUserId = patientUser.Id
            };

            ServiceProvider.GetRequiredService<IWriteRepository<Patient>>()
                .AddAsync(patient, default)
                .GetAwaiter()
                .GetResult();
            ServiceProvider.GetRequiredService<IUnitOfWork>().Save();

            return patient;
        }

        private TreatmentSession CreateTreatmentSession(
            Guid patientId,
            Guid dentalTeamId,
            Guid treatmentId,
            DateTimeOffset start,
            DateTimeOffset end,
            TreatmentSessionStatus status)
        {
            var treatmentSession = new TreatmentSession
            {
                PatientId = patientId,
                DentalTeamId = dentalTeamId,
                TreatmentId = treatmentId,
                Start = start,
                End = end,
                Status = status
            };

            ServiceProvider.GetRequiredService<IWriteRepository<TreatmentSession>>()
                .AddAsync(treatmentSession, default)
                .GetAwaiter()
                .GetResult();

            ServiceProvider.GetRequiredService<IUnitOfWork>().Save();

            return ServiceProvider.GetRequiredService<IReadRepository<TreatmentSession>>()
                .AsNoTracking()
                .Include(ts => ts.Patient)
                .Include(ts => ts.DentalTeam)
                .Include(ts => ts.Treatment)
                .FirstOrDefault();
        }
    }
}