using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Common.Validation;
using DentalSystem.Application.UseCases.Identity.Dto.Input;
using DentalSystem.Application.UseCases.Identity.Dto.Output;
using DentalSystem.Presentation.Web.Api.Tests.Common.Steps;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DentalSystem.Presentation.Web.Api.Tests.Identity.ProfileManagement.Steps
{
    [Binding]
    public class UpdateProfileWithInvalidInputScenarioSteps
    {
        private const string UpdateProfileInputErrorsLabel = "UpdateProfileInputErrors";
        private const string UserInputAccessTokenPairsLabel = "UserInputAccessTokenPairs";

        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public UpdateProfileWithInvalidInputScenarioSteps(
            HttpClient httpClient,
            ScenarioContext scenarioContext)
        {
            _httpClient = httpClient;
            _scenarioContext = scenarioContext;
        }

        [When(@"Update profile with invalid details")]
        public void WhenUpdateProfileWithInvalidDetails(Table table)
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

            var errors = queries
                .Select(async q => await q.Result.Content.ReadFromJsonAsync<List<ValidationError>>())
                .Select(r => r.Result)
                .ToList();

            _scenarioContext.Add(UpdateProfileInputErrorsLabel, errors);
        }

        [Then(@"Should get user profile input errors")]
        public void ThenShouldGetUserProfileInputErrors()
        {
            var errors = _scenarioContext.Get<List<List<ValidationError>>>(UpdateProfileInputErrorsLabel);
            errors.Should().OnlyContain(err => err.Count > 0);
        }
    }
}