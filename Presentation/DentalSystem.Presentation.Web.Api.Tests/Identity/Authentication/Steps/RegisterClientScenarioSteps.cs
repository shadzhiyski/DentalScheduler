using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Identity.Dto.Input;
using DentalSystem.Application.UseCases.Identity.Dto.Output;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DentalSystem.Presentation.Web.Api.Tests.Identity.Authentication.Steps
{
    [Binding]
    public class RegisterClientScenarioSteps
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public RegisterClientScenarioSteps(HttpClient httpClient, ScenarioContext scenarioContext)
        {
            _httpClient = httpClient;
            _scenarioContext = scenarioContext;
        }

        [When(@"Register client with user details")]
        public void RegisterClientWithUserDetails(Table table)
        {
            var inputSet = table.CreateSet<UserCredentialsInput>();
            var queries = inputSet
                .ToList()
                .Select(uc => _httpClient.PostAsJsonAsync("api/Auth/register", uc))
                .ToArray();

            Task.WaitAll(queries);

            var accessTokens = queries
                .Select(async r => await r)
                .Select(r => r.Result.Content)
                .Select(async r => await r.ReadFromJsonAsync<AccessTokenOutput>())
                .Select(r => r.Result)
                .ToList();

            _scenarioContext.Add("AccessTokens", accessTokens);
        }

        [Then(@"Should receive client access token")]
        public void ThenShouldReceiveClientAccessToken()
        {
            var accessTokens = _scenarioContext
                .Get<List<AccessTokenOutput>>("AccessTokens");
            accessTokens.Should().OnlyContain(r => r.AccessToken != default);
        }
    }
}