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

namespace DentalSystem.Presentation.Web.Api.Tests.Steps
{
    [Binding]
    public class IdentitySteps
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public IdentitySteps(HttpClient httpClient, ScenarioContext scenarioContext)
        {
            _httpClient = httpClient;
            _scenarioContext = scenarioContext;
        }

        [When(@"Login with user details")]
        public void WhenLoginWithUserDetails(Table table)
        {
            var inputSet = table.CreateSet<UserCredentialsInput>();
            var queries = inputSet
                .ToList()
                .Select(uc => _httpClient.PostAsJsonAsync("api/Auth/login", uc))
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

        [Then(@"Should receive access token")]
        public void ThenShouldReceiveAccessToken()
        {
            var accessTokens = _scenarioContext
                .Get<List<AccessTokenOutput>>("AccessTokens");
            accessTokens.Should().OnlyContain(r => r.AccessToken != default);
        }
    }
}