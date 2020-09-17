using System;
using System.Linq;
using DentalScheduler.DTO.Input;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.UseCases;
using DentalScheduler.UseCases.Tests.Utilities;
using DentalScheduler.UseCases.Tests.Utilities.DataProviders;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DentalScheduler.UseCases.Tests.TreatmentSession
{
    public class AddTreatmentSessionCommandTests : BaseIntegrationTests
    {
        public AddTreatmentSessionCommandTests() : base(new ServiceCollection())
        {
            Sut = ServiceProvider.GetRequiredService<IAddTreatmentSessionCommand>();
            
            var (_, patient) = ServiceProvider.GetRequiredService<IUserDbDataProvider>()
                .ProvidePatient("patient", "patient#123");
            Patient = patient;

            DentalTeam = ServiceProvider.GetRequiredService<IDentalTeamDbDataProvider>()
                .ProvideDentalTeam("Dental Team 1", "Room 1", "dentist");
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
            messageOutput.Message.Should().BeEquivalentTo("Treatment Session successfully added.");
        }
    }
}