using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Common.Validation;
using DentalSystem.Application.UseCases.Identity.Dto.Input;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DentalSystem.Presentation.Web.Api.Tests.Identity.Authentication.Steps
{
    [Binding]
    public class LoginInvalidUserScenarioSteps
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public LoginInvalidUserScenarioSteps(HttpClient httpClient, ScenarioContext scenarioContext)
        {
            _httpClient = httpClient;
            _scenarioContext = scenarioContext;
        }

        [When(@"Login with invalid user details")]
        public void WhenLoginWithInvalidUserDetails(Table table)
        {
            var inputSet = table.CreateSet<UserCredentialsInput>();
            var queries = inputSet
                .ToList()
                .Select(uc => _httpClient.PostAsJsonAsync("api/Auth/login", uc))
                .ToArray();

            Task.WaitAll(queries);

            var errors = queries
                .Select(async r => await r)
                .Select(r => r.Result.Content)
                .Select(async r => await r.ReadFromJsonAsync<List<ValidationError>>())
                .Select(r => r.Result)
                .ToList();

            _scenarioContext.Add("Errors", errors);
        }

        [Then(@"Should receive invalid user input errors")]
        public void ThenShouldReceiveInvalidUserInputErrors()
        {
            var errors = _scenarioContext
                .Get<List<List<ValidationError>>>("Errors");
            errors.Should().OnlyContain(el => el.Count > 0);
        }
    }
}