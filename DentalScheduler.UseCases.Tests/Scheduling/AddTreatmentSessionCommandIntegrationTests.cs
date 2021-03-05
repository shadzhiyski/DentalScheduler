using System;
using System.Linq;
using DentalScheduler.UseCases.Scheduling.Dto.Input;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.UseCases.Scheduling.Commands;
using DentalScheduler.UseCases.Tests.Utilities;
using DentalScheduler.UseCases.Tests.Utilities.DataProviders;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DentalScheduler.UseCases.Tests.Scheduling
{
    public class AddTreatmentSessionCommandIntegrationTests : BaseIntegrationTests
    {
        public AddTreatmentSessionCommandIntegrationTests() : base(new ServiceCollection())
        {
            Sut = ServiceProvider.GetRequiredService<IAddTreatmentSessionCommand>();
            
            var (_, patient) = ServiceProvider.GetRequiredService<IUserDbDataProvider>()
                .ProvidePatientAsync("patient", "patient#123")
                .GetAwaiter()
                .GetResult();
            Patient = patient;

            DentalTeam = ServiceProvider.GetRequiredService<IDentalTeamDbDataProvider>()
                .ProvideDentalTeamAsync("Dental Team 1", "Room 1", "dentist")
                .GetAwaiter()
                .GetResult();
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
    }
}