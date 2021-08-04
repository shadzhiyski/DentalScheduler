using System;
using System.Linq;
using DentalSystem.Application.UseCases.Scheduling.Dto.Input;
using DentalSystem.Application.UseCases.Tests.Common;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using DentalSystem.Domain.Scheduling.Entities;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using DentalSystem.Domain.Identity;
using DentalSystem.Application.Boundaries.Infrastructure.Identity;
using Microsoft.Extensions.Localization;
using DentalSystem.Application.UseCases.Scheduling.Validation;
using System.Collections.Generic;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;
using DentalSystem.Application.UseCases.Common.Validation;
using MediatR;

namespace DentalSystem.Application.UseCases.Tests.Scheduling
{
    public class AddTreatmentSessionCommandIntegrationTests : BaseIntegrationTests
    {
        public AddTreatmentSessionCommandIntegrationTests() : base(new ServiceCollection())
        {
            Sut = ServiceProvider.GetRequiredService<IMediator>();

            Patient = CreatePatientUser();

            DentalTeam = ServiceProvider.GetRequiredService<IReadRepository<DentalTeam>>()
                .AsNoTracking()
                .SingleOrDefault(dt => dt.Name == "DentalTeam 01");
        }

        public Patient Patient { get; }

        public DentalTeam DentalTeam { get; }

        public IMediator Sut { get; }

        [Fact]
        public async void AddTreatmentSession_ValidInput_ShouldReturnSuccessfullMessage()
        {
            // Arrange
            var input = new TreatmentSessionInput()
                {
                    TreatmentReferenceId = Treatments.First().ReferenceId,
                    PatientReferenceId = Patient.ReferenceId,
                    DentalTeamReferenceId = DentalTeam.ReferenceId,
                    Start = DateTimeOffset.UtcNow,
                    Status = "Requested"
                };
            input = input
                with
                {
                    End = input.Start.Value.AddHours(1)
                };

            // Act
            var result = await Sut.Send(input, default);
            var messageOutput = result.Value;

            // Assert
            messageOutput.Message.Should().BeEquivalentTo("Treatment Session successfully created.");
        }

        [Fact]
        public async void AddTreatmentSession_InvalidInput_ShouldReturnErrorResult()
        {
            // Arrange
            var input = new TreatmentSessionInput();

            // Act
            var result = await Sut.Send(input, default);

            // Assert
            var allValidationMessages = ServiceProvider.GetRequiredService<IStringLocalizer<TreatmentSessionValidator>>()
                .GetAllStrings()
                .Union(ServiceProvider.GetRequiredService<IStringLocalizer<PeriodValidator>>().GetAllStrings())
                .Select(ls => ls.Value);
            var allUniqueValidationMessages = new HashSet<string>(allValidationMessages);
            result.Errors
                .Cast<IValidationError>()
                .SelectMany(ve => ve.Errors)
                .Should()
                .OnlyContain(errMsg => allUniqueValidationMessages.Contains(errMsg));
        }

        [Fact]
        public async void AddTreatmentSession_ValidInputViolatingBusinessRules_ShouldReturnErrorResult()
        {
            // Arrange
            var input = new TreatmentSessionInput()
                {
                    TreatmentReferenceId = Treatments.First().ReferenceId,
                    PatientReferenceId = Patient.ReferenceId,
                    DentalTeamReferenceId = DentalTeam.ReferenceId,
                    Start = DateTimeOffset.UtcNow,
                    Status = "Requested"
                };
            input = input
                with
                {
                    End = input.Start.Value.AddHours(1)
                };

            // Act
            await Sut.Send(input, default);
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
    }
}