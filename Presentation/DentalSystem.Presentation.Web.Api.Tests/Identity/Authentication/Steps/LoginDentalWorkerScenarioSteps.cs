using System.Net.Http;
using TechTalk.SpecFlow;

namespace DentalSystem.Presentation.Web.Api.Tests.Identity.Authentication.Steps
{
    [Binding]
    public class LoginDentalWorkerScenarioSteps : BaseLoginSteps
    {
        public LoginDentalWorkerScenarioSteps(HttpClient httpClient, ScenarioContext scenarioContext)
            : base(httpClient, scenarioContext)
        { }

        [When(@"Login with dental worker user details")]
        public void WhenLoginWithDentalWorkerUserDetails(Table table)
            => LoginUsers(table);

        [Then(@"Should receive dental worker access token")]
        public void ThenShouldReceiveDentalWorkerAccessToken()
            => ShouldReceiveAccessToken();
    }
}