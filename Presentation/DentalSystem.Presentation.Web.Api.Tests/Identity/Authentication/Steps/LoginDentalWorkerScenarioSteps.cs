using System.Net.Http;
using DentalSystem.Presentation.Web.Api.Tests.Common.Steps;
using TechTalk.SpecFlow;

namespace DentalSystem.Presentation.Web.Api.Tests.Identity.Authentication.Steps
{
    [Binding]
    public class LoginDentalWorkerScenarioSteps
    {
        private readonly LoginStep _loginStep;
        private readonly ShouldReceiveAccessTokenStep _shouldReceiveAccessTokenStep;

        public LoginDentalWorkerScenarioSteps(
            LoginStep loginStep,
            ShouldReceiveAccessTokenStep shouldReceiveAccessTokenStep)
        {
            _loginStep = loginStep;
            _shouldReceiveAccessTokenStep = shouldReceiveAccessTokenStep;
        }

        [When(@"Login with dental worker user details")]
        public void WhenLoginWithDentalWorkerUserDetails(Table table)
            => _loginStep.LoginUsers(table);

        [Then(@"Should receive dental worker access token")]
        public void ThenShouldReceiveDentalWorkerAccessToken()
            => _shouldReceiveAccessTokenStep.ShouldReceiveAccessToken();
    }
}