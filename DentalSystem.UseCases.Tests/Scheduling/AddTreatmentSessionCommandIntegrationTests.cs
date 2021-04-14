using System;
using System.Linq;
using DentalSystem.UseCases.Scheduling.Dto.Input;
using DentalSystem.Interfaces.UseCases.Scheduling.Commands;
using DentalSystem.UseCases.Tests.Utilities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Interfaces.Infrastructure.Common.Persistence;
using DentalSystem.Entities.Identity;
using DentalSystem.Interfaces.Infrastructure.Identity;

namespace DentalSystem.UseCases.Tests.Scheduling
{
    public class AddTreatmentSessionCommandIntegrationTests : BaseIntegrationTests
    {
        public AddTreatmentSessionCommandIntegrationTests() : base(new ServiceCollection())
        {
            Sut = ServiceProvider.GetRequiredService<IAddTreatmentSessionCommand>();

            Patient = CreatePatientUser();

            DentalTeam = ServiceProvider.GetRequiredService<IGenericRepository<DentalTeam>>()
                .AsNoTracking()
                .SingleOrDefault(dt => dt.Name == "DentalTeam 01");
        }

        public Patient Patient { get; }

        public DentalTeam DentalTeam { get; }

        public IAddTreatmentSessionCommand Sut { get; }

        [Fact]
        public async void AddTreatmentSession_ValidInput_ShouldReturnSuccessfullMessage()
        {
            // Arrange
            var input = new TreatmentSessionInput();
            input.TreatmentReferenceId = Treatments.First().ReferenceId;
            input.PatientReferenceId = Patient.ReferenceId;
            input.DentalTeamReferenceId = DentalTeam.ReferenceId;
            input.Start = DateTimeOffset.UtcNow;
            input.End = input.Start.Value.AddHours(1);
            input.Status = "Requested";

            // Act
            var result = await Sut.ExecuteAsync(input);
            var messageOutput = result.Value;

            // Assert
            messageOutput.Message.Should().BeEquivalentTo("Treatment Session successfully created.");
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
            userService.CreateAsync(patientUser, "patient#123")
                .GetAwaiter()
                .GetResult();
            userService.AddToRoleAsync(patientUser, "Patient")
                .GetAwaiter()
                .GetResult();

            var patient = new Patient
            {
                IdentityUserId = patientUser.Id
            };

            ServiceProvider.GetRequiredService<IGenericRepository<Patient>>()
                .AddAsync(patient)
                .GetAwaiter()
                .GetResult();
            ServiceProvider.GetRequiredService<IUnitOfWork>().Save();

            return patient;
        }
    }
}