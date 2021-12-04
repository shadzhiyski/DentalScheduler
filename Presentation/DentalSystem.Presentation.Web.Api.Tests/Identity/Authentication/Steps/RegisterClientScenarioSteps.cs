using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Common.Validation;
using DentalSystem.Application.UseCases.Identity.Dto.Input;
using DentalSystem.Application.UseCases.Identity.Dto.Output;
using DentalSystem.Presentation.Web.Api.Tests.Common.Steps;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DentalSystem.Presentation.Web.Api.Tests.Identity.Authentication.Steps
{
    [Binding]
    public class RegisterClientScenarioSteps
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;
        private readonly ShouldReceiveAccessTokenStep _shouldReceiveAccessTokenStep;

        public RegisterClientScenarioSteps(
            HttpClient httpClient,
            ScenarioContext scenarioContext,
            ShouldReceiveAccessTokenStep shouldReceiveAccessTokenStep)
        {
            _httpClient = httpClient;
            _shouldReceiveAccessTokenStep = shouldReceiveAccessTokenStep;
            _scenarioContext = scenarioContext;
        }

        [When(@"Register client with user details")]
        public async Task RegisterClientWithUserDetails(Table table)
        {
            var inputSet = table.CreateSet<UserCredentialsInput>();
            var queries = inputSet
                .ToList()
                .Select(uc => _httpClient.PostAsJsonAsync("api/Auth/register", uc))
                .ToArray();

            await Task.WhenAll(queries);

            var responses = queries
                .Select(r => r.Result)
                .Select(ReadResponseAsync)
                .ToArray();

            await Task.WhenAll(responses);

            var results = responses
                .Select(r => r.Result)
                .ToList();

            var accessTokens = results
                .Where(r => r.AccessToken != default)
                .Select(r => r.AccessToken)
                .ToList();
            var errors = results
                .Where(r => r.Errors != default)
                .Select(r => r.Errors)
                .ToList();

            _scenarioContext.Add(LoginStep.AccessTokensLabel, accessTokens);
            _scenarioContext.Add(LoginStep.ErrorsLabel, errors);
        }

        [Then(@"Should receive client access token")]
        public void ThenShouldReceiveClientAccessToken()
            => _shouldReceiveAccessTokenStep.ShouldReceiveAccessToken();

        private async Task<(AccessTokenOutput AccessToken, List<ValidationError> Errors)> ReadResponseAsync(
                HttpResponseMessage response)
            => HttpStatusCode.OK.Equals(response.StatusCode)
                ? (AccessToken: await response.Content.ReadFromJsonAsync<AccessTokenOutput>(), Errors: default)
                : (AccessToken: default, Errors: await response.Content.ReadFromJsonAsync<List<ValidationError>>());
    }
}