using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Common.Validation;
using DentalSystem.Application.UseCases.Identity.Dto.Input;
using DentalSystem.Application.UseCases.Identity.Dto.Output;
using TechTalk.SpecFlow;

namespace DentalSystem.Presentation.Web.Api.Tests.Common.Steps
{
    public class RegisterUserStep
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public RegisterUserStep(HttpClient httpClient, ScenarioContext scenarioContext)
        {
            _httpClient = httpClient;
            _scenarioContext = scenarioContext;
        }

        public async Task RegisterUser(IEnumerable<UserCredentialsInput> inputSet)
        {
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

        public async Task RegisterUser(UserCredentialsInput credentials)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/register", credentials);

            (AccessTokenOutput accessToken, List<ValidationError> errors) = await ReadResponseAsync(response);

            if (accessToken != default)
            {
                _scenarioContext.Add(LoginStep.AccessTokenLabel, accessToken);
            }

            if (errors != default)
            {
                _scenarioContext.Add(LoginStep.SingleCallErrorsLabel, errors);
            }
        }

        private async Task<(AccessTokenOutput AccessToken, List<ValidationError> Errors)> ReadResponseAsync(
                HttpResponseMessage response)
            => HttpStatusCode.OK.Equals(response.StatusCode)
                ? (AccessToken: await response.Content.ReadFromJsonAsync<AccessTokenOutput>(), Errors: default)
                : (AccessToken: default, Errors: await response.Content.ReadFromJsonAsync<List<ValidationError>>());
    }
}