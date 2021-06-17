using System.Net.Http;
using TechTalk.SpecFlow;

namespace DentalSystem.Presentation.Web.Api.Tests.Identity.Authentication.Steps
{
    [Binding]
    public class LoginRegisteredClientScenarioSteps : BaseLoginSteps
    {
        public LoginRegisteredClientScenarioSteps(HttpClient httpClient, ScenarioContext scenarioContext)
            : base(httpClient, scenarioContext)
        { }

        [When(@"Login client with user details")]
        public void WhenLoginWithDentalWorkerUserDetails(Table table)
            => LoginUsers(table);

        [Then(@"Should receive registered client access token")]
        public void ThenShouldReceiveDentalWorkerAccessToken()
            => ShouldReceiveAccessToken();
    }
}