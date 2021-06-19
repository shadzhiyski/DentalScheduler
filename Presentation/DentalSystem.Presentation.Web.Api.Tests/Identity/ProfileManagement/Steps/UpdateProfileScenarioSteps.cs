using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Identity.Dto.Input;
using DentalSystem.Application.UseCases.Identity.Dto.Output;
using DentalSystem.Presentation.Web.Api.Tests.Common.Steps;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DentalSystem.Presentation.Web.Api.Tests.Identity.ProfileManagement.Steps
{
    [Binding]
    public class UpdateProfileScenarioSteps
    {
        private const string ResultHttpStatusCodesLabel = "ResultHttpStatusCodes";
        private const string UserInputAccessTokenPairsLabel = "UserInputAccessTokenPairs";
        private const string UserProfileOutputsLabel = "UserProfileOutputs";

        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;
        private readonly LoginStep _loginStep;

        public UpdateProfileScenarioSteps(
            HttpClient httpClient,
            ScenarioContext scenarioContext,
            LoginStep loginStep)
        {
            _httpClient = httpClient;
            _loginStep = loginStep;
            _scenarioContext = scenarioContext;
        }

        [Given(@"Authorized user with auth token")]
        public void GivenAuthorizedUserWithAuthToken(Table table)
            => _loginStep.LoginUsers(table);

        [When(@"Update profile with valid details")]
        public void WhenLoginWithInvalidUserDetails(Table table)
        {
            var inputSet = table.CreateSet<UserProfileInput>();
            var accessTokens = _scenarioContext.Get<List<AccessTokenOutput>>(LoginStep.AccessTokensLabel);
            var userInputAccessTokenPairs = inputSet.Zip(
                    accessTokens,
                    (input, accessToken) => (UserInput: input, AccessToken: accessToken.AccessToken)
                )
                .ToList();
            _scenarioContext.Add(UserInputAccessTokenPairsLabel, userInputAccessTokenPairs);

            var queries = userInputAccessTokenPairs
                .Select(ia =>
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", ia.AccessToken);

                    var input  = ia.UserInput;
                    var content = new MultipartFormDataContent();
                    content.Add(
                        content: new ByteArrayContent(new byte[0], 0, 0),
                        name: "Avatar",
                        fileName: "Avatar"
                    );
                    content.Add(content: new StringContent(input.FirstName), name: nameof(input.FirstName));
                    content.Add(content: new StringContent(input.LastName), name: nameof(input.LastName));

                    return _httpClient.PostAsync("api/User/profile", content);
                })
                .ToArray();

            Task.WaitAll(queries);

            var results = queries
                .Select(q => q.Result.StatusCode)
                .ToList();

            _scenarioContext.Add(ResultHttpStatusCodesLabel, results);
        }

        [Then(@"Should update profile info successfully")]
        public void ThenShouldUpdateProfileInfoSuccessfully()
        {
            var statusCodes = _scenarioContext.Get<List<HttpStatusCode>>(ResultHttpStatusCodesLabel);
            statusCodes.Should().OnlyContain(sc => HttpStatusCode.OK.Equals(sc));
        }

        [When(@"Get profile info")]
        public void WhenGetProfileInfo()
        {
            var userInputAccessTokenPairs = _scenarioContext
                .Get<List<(UserProfileInput UserInput, string AccessToken)>>(UserInputAccessTokenPairsLabel);

            var getQueries = userInputAccessTokenPairs
                .Select(ia =>
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", ia.AccessToken);
                    return _httpClient.GetFromJsonAsync<UserProfileOutput>("api/User/profile");
                })
                .ToArray();

            Task.WaitAll(getQueries);

            var userProfileOutputs = getQueries
                .Select(q => q.Result)
                .ToList();

            _scenarioContext.Add(UserProfileOutputsLabel, userProfileOutputs);
        }

        [Then(@"Should get updated profile info")]
        public void ThenShouldGetUpdatedProfileInfo()
        {
            var userProfileOutputs = _scenarioContext
                .Get<List<UserProfileOutput>>(UserProfileOutputsLabel)
                .Select(r => (FirstName: r.FirstName, LastName: r.LastName));

            var userInputData = _scenarioContext
                .Get<List<(UserProfileInput UserInput, string AccessToken)>>(UserInputAccessTokenPairsLabel)
                .Select(ia => ia.UserInput)
                .Select(ui => (FirstName: ui.FirstName, LastName: ui.LastName));

            userInputData.Should().ContainInOrder(userProfileOutputs);
        }
    }
}