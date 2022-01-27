using System.Net.Http;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Identity.Dto.Input;
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
        private readonly RegisterUserStep _registerUserStep;
        private readonly ShouldReceiveAccessTokenStep _shouldReceiveAccessTokenStep;

        public RegisterClientScenarioSteps(
            HttpClient httpClient,
            ScenarioContext scenarioContext,
            RegisterUserStep registerUserStep,
            ShouldReceiveAccessTokenStep shouldReceiveAccessTokenStep)
        {
            _httpClient = httpClient;
            _shouldReceiveAccessTokenStep = shouldReceiveAccessTokenStep;
            _scenarioContext = scenarioContext;
            _registerUserStep = registerUserStep;
        }

        [When(@"Register client with user details")]
        public Task RegisterClientWithUserDetails(Table table)
            => _registerUserStep.RegisterUser(table.CreateSet<UserCredentialsInput>());

        [Then(@"Should receive client access token")]
        public void ThenShouldReceiveClientAccessToken()
            => _shouldReceiveAccessTokenStep.ShouldReceiveAccessToken();
    }
}