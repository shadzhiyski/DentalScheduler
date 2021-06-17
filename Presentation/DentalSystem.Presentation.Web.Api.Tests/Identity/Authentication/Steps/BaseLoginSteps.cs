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
    public class BaseLoginSteps
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public BaseLoginSteps(HttpClient httpClient, ScenarioContext scenarioContext)
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

            var accessTokens = queries
                .Select(async r => await r)
                .Select(r => r.Result.Content)
                .Select(async r => await r.ReadFromJsonAsync<AccessTokenOutput>())
                .Select(r => r.Result)
                .ToList();

            _scenarioContext.Add("AccessTokens", accessTokens);
        }

        public void ShouldReceiveAccessToken()
        {
            var accessTokens = _scenarioContext
                .Get<List<AccessTokenOutput>>("AccessTokens");
            accessTokens.Should().OnlyContain(r => r.AccessToken != default);
        }
    }
}