using DentalSystem.Presentation.Web.Api.Tests.Common.Steps;
using TechTalk.SpecFlow;

namespace DentalSystem.Presentation.Web.Api.Tests.Identity.Authentication.Steps
{
    [Binding]
    public class LoginInvalidUserScenarioSteps
    {
        private readonly LoginStep _loginStep;
        private readonly ShouldReceiveLoginErrorsStep _shouldReceiveLoginErrorsStep;

        public LoginInvalidUserScenarioSteps(
            LoginStep loginStep,
            ShouldReceiveLoginErrorsStep shouldReceiveLoginErrorsStep)
        {
            _shouldReceiveLoginErrorsStep = shouldReceiveLoginErrorsStep;
            _loginStep = loginStep;
        }

        [When (@"Login with invalid user details")]
        public void WhenLoginWithInvalidUserDetails(Table table)
            => _loginStep.LoginUsers(table);

        [Then (@"Should receive invalid user input errors")]
        public void ThenShouldReceiveInvalidUserInputErrors()
            => _shouldReceiveLoginErrorsStep.ShouldReceiveLoginErrors();
    }
}