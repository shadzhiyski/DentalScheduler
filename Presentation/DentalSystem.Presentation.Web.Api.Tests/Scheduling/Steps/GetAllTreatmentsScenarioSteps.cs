using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Scheduling.Dto.Output;
using FluentAssertions;
using Simple.OData.Client;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DentalSystem.Presentation.Web.Api.Tests.Scheduling.Steps
{
    [Binding]
    public class GetAllTreatmentsScenarioSteps
    {
        private readonly ODataClient _oDataClient;
        private readonly ScenarioContext _scenarioContext;

        public GetAllTreatmentsScenarioSteps(
            ODataClient oDataClient,
            ScenarioContext scenarioContext)
        {
            _oDataClient = oDataClient;
            _scenarioContext = scenarioContext;
        }

        [When(@"Request all provided treatments by the system")]
        public async Task WhenRequestAllProvidedTreatmentsByTheSystem()
        {
            var records = (await _oDataClient
                .For<TreatmentOutput>("Treatment")
                .FindEntriesAsync())
                .ToList();

            _scenarioContext.Add("treatments", records);
        }

        [Then(@"Should retrieve all provided treatments")]
        public void ThenShouldRetrieveAllProvidedTreatments(Table table)
        {
            var expectedTreatments = table.CreateSet<TreatmentOutput>();
            var resultTreatments = _scenarioContext
                .Get<List<TreatmentOutput>>("treatments")
                .Select(t => (t.Name, t.DurationInMinutes));

            expectedTreatments
                .Select(t => (t.Name, t.DurationInMinutes))
                .Should()
                .BeEquivalentTo(resultTreatments);
        }
    }
}