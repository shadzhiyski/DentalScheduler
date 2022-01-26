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
using TechTalk.SpecFlow.Assist;

namespace DentalSystem.Presentation.Web.Api.Tests.Common.Steps
{
    public class LoginStep
    {
        public const string AccessTokenLabel = "AccessToken";
        public const string AccessTokensLabel = "AccessTokens";
        public const string SingleCallErrorsLabel = "SingleCallErrors";
        public const string ErrorsLabel = "Errors";

        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public LoginStep(HttpClient httpClient, ScenarioContext scenarioContext)
        {
            _httpClient = httpClient;
            _scenarioContext = scenarioContext;
        }

        public void LoginUsers(Table table)
        {
            var inputSet = table.CreateSet<UserCredentialsInput>();
            var queries = inputSet
                .ToList()
                .Select(uc => _httpClient.PostAsJsonAsync("api/Auth/login", uc))
                .ToArray();

            Task.WaitAll(queries);

            var results = queries
                .Select(r => r.Result)
                .Select(ReadResponseAsync)
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

            _scenarioContext.Add(AccessTokensLabel, accessTokens);
            _scenarioContext.Add(ErrorsLabel, errors);
        }

        private async Task<(AccessTokenOutput AccessToken, List<ValidationError> Errors)> ReadResponseAsync(
                HttpResponseMessage response)
            => HttpStatusCode.OK.Equals(response.StatusCode)
                ? (AccessToken: await response.Content.ReadFromJsonAsync<AccessTokenOutput>(), Errors: default)
                : (AccessToken: default, Errors: await response.Content.ReadFromJsonAsync<List<ValidationError>>());
    }
}