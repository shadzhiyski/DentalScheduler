using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BoDi;
using DentalSystem.Application.UseCases.Common.Validation;
using DentalSystem.Application.UseCases.Identity.Dto.Input;
using DentalSystem.Application.UseCases.Identity.Dto.Output;
using DentalSystem.Application.UseCases.Scheduling.Dto.Input;
using DentalSystem.Application.UseCases.Scheduling.Dto.Output;
using DentalSystem.Presentation.Web.Api.Tests.Common.Handlers;
using DentalSystem.Presentation.Web.Api.Tests.Common.Steps;
using FluentAssertions;
using Simple.OData.Client;
using TechTalk.SpecFlow;

namespace DentalSystem.Presentation.Web.Api.Tests.Scheduling.Steps
{
    [Binding]
    public class RequestTreatmentSessionScenarioSteps
    {
        public const string PatientReferenceIdLabel = "PatientReferenceId";
        public const string DentalTeamReferenceIdLabel = "DentalTeamReferenceId";
        public const string TreatmentReferenceIdLabel = "TreatmentReferenceId";
        private const string ValidationErrorsLabel = "ValidationErrors";
        private const string TreatmentSessionInputLabel = "TreatmentSessionInput";
        private readonly IObjectContainer _objectContainer;
        private readonly HttpClient _httpClient;
        private readonly ODataClient _oDataClient;
        private readonly ScenarioContext _scenarioContext;
        private readonly RegisterUserStep _registerUserStep;

        public RequestTreatmentSessionScenarioSteps(
            IObjectContainer objectContainer,
            HttpClient httpClient,
            ODataClient oDataClient,
            ScenarioContext scenarioContext,
            RegisterUserStep registerUserStep)
        {
            _objectContainer = objectContainer;
            _httpClient = objectContainer.Resolve<HttpClient>("Authorized");
            _oDataClient = objectContainer.Resolve<ODataClient>("Authorized");
            _scenarioContext = scenarioContext;
            _registerUserStep = registerUserStep;
        }

        [Given(@"Register client ""(.*)"" with password ""(.*)""")]
        public Task GivenRegisterClientWithPassword(string userName, string password)
            => _registerUserStep.RegisterUser(new UserCredentialsInput { UserName = userName, Password = password });

        [Given(@"Patient ReferenceId of authorized user")]
        public void GivenPatientReferenceIdOfAuthorizedUser()
        {
            var accessToken = _scenarioContext
                .Get<AccessTokenOutput>(LoginStep.AccessTokenLabel);
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken.AccessToken);

            var patientReferenceIdAsString = jwtSecurityToken.Claims
                .FirstOrDefault(c => c.Type.Equals("PatientReferenceId"))
                ?.Value;
            var patientReferenceId = patientReferenceIdAsString != null
                ? new Guid(patientReferenceIdAsString)
                : Guid.Empty;

            _scenarioContext.Add(PatientReferenceIdLabel, patientReferenceId);
        }

        [Given(@"Have requested treatment session for ""(.*)"" ""(.*)"", treatment ""(.*)"" and dental team ""(.*)""")]
        public async Task GivenHaveRequestedTreatmentSessionForTreatmentAndDentalTeam(
            DateTimeOffset start, DateTimeOffset end, string treatmentName, string dentalTeamName)
        {
            // var authHandler = _objectContainer.Resolve<AuthorizationHeaderHttpHandler>();
            // authHandler.ScenarioContext = _scenarioContext;

            var patientReferenceId = _scenarioContext.Get<Guid>(PatientReferenceIdLabel);
            var treatmentReferenceId = await _oDataClient
                .For<TreatmentOutput>("Treatment")
                .Filter(dt => dt.Name == treatmentName)
                .Select(dt => dt.ReferenceId)
                .FindScalarAsync<Guid>();

            var dentalTeamReferenceId = await _oDataClient
                .For<DentalTeamOutput>("DentalTeam")
                .Filter(dt => dt.Name == dentalTeamName)
                .Select(dt => dt.ReferenceId)
                .FindScalarAsync<Guid>();

            var input = new TreatmentSessionInput
            {
                Start = start,
                End = end,
                TreatmentReferenceId = treatmentReferenceId,
                DentalTeamReferenceId = dentalTeamReferenceId,
                PatientReferenceId = patientReferenceId
            };

            var res = await _httpClient.PostAsJsonAsync("odata/TreatmentSession", input);
        }

        [Given(@"Dental team ReferenceId of ""(.*)""")]
        public async Task GivenDentalTeamReferenceIdOf(string dentalTeamName)
        {
            // var authHandler = _objectContainer.Resolve<AuthorizationHeaderHttpHandler>();
            // authHandler.ScenarioContext = _scenarioContext; // TODO: find ScenarioContext provider ???

            var dentalTeamReferenceId = await _oDataClient
                .For<DentalTeamOutput>("DentalTeam")
                .Filter(dt => dt.Name == dentalTeamName)
                .Select(dt => dt.ReferenceId)
                .FindScalarAsync<Guid>();

            _scenarioContext.Add(DentalTeamReferenceIdLabel, dentalTeamReferenceId);
        }

        [Given(@"Treatment ReferenceId of ""(.*)""")]
        public async Task GivenTreatmentReferenceIdOf(string treatmentName)
        {
            // var authHandler = _objectContainer.Resolve<AuthorizationHeaderHttpHandler>();
            // authHandler.ScenarioContext = _scenarioContext;

            var treatmentReferenceId = await _oDataClient
                .For<TreatmentOutput>("Treatment")
                .Filter(dt => dt.Name == treatmentName)
                .Select(dt => dt.ReferenceId)
                .FindScalarAsync<Guid>();

            _scenarioContext.Add(TreatmentReferenceIdLabel, treatmentReferenceId);
        }

        [Given(@"Treatment session details ""(.*)"" ""(.*)""")]
        public void GivenTreatmentSessionDetails(DateTimeOffset start, DateTimeOffset end)
        {
            var dentalTeamReferenceId = _scenarioContext.Get<Guid>(DentalTeamReferenceIdLabel);
            var treatmentReferenceId = _scenarioContext.Get<Guid>(TreatmentReferenceIdLabel);
            var patientReferenceId = _scenarioContext.Get<Guid>(PatientReferenceIdLabel);
            var input = new TreatmentSessionInput
                {
                    Start = start,
                    End = end,
                    TreatmentReferenceId = treatmentReferenceId,
                    DentalTeamReferenceId = dentalTeamReferenceId,
                    PatientReferenceId = patientReferenceId
                };

            _scenarioContext.Add(TreatmentSessionInputLabel, input);
        }

        [When(@"Request treatment session")]
        public async Task WhenRequestTreatmentSession()
        {
            var allTreatmentSessions = (await _oDataClient
                    .For<TreatmentSessionOutput>("TreatmentSession")
                    .FindEntriesAsync()
                )
                .ToList();

            var input = _scenarioContext.Get<TreatmentSessionInput>(TreatmentSessionInputLabel);
            var response = await _httpClient.PostAsJsonAsync("odata/TreatmentSession", input);

            var validationErrors = new List<ValidationError>();
            if (!HttpStatusCode.Created.Equals(response.StatusCode))
            {
                validationErrors = await response.Content.ReadFromJsonAsync<List<ValidationError>>();
            }

            _scenarioContext.Add(ValidationErrorsLabel, validationErrors);
        }

        [Then(@"Should create treatment session successfully")]
        public void ShouldCreateTreatmentSessionSuccessfully()
        {
            var validationErrors = _scenarioContext.Get<List<ValidationError>>(ValidationErrorsLabel);
            validationErrors.Should().BeEmpty();
        }

        [Then(@"Should fail to create treatment session")]
        public void ShouldFailToCreateTreatmentSession()
        {
            var validationErrors = _scenarioContext.Get<List<ValidationError>>(ValidationErrorsLabel);
            validationErrors.Should().NotBeEmpty();
        }
    }
}