using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Identity.Dto.Output;
using DentalSystem.Presentation.Web.Api.Tests.Common.Steps;
using TechTalk.SpecFlow;

namespace DentalSystem.Presentation.Web.Api.Tests.Common.Handlers
{
    public class AuthorizationHeaderHttpHandler : DelegatingHandler
    {
        public AuthorizationHeaderHttpHandler(ScenarioContext scenarioContext)
        {
            InnerHandler = new HttpClientHandler();
            ScenarioContext = scenarioContext;
        }

        public ScenarioContext ScenarioContext { get; init; }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization == default
                && ScenarioContext != default
                && ScenarioContext.ContainsKey(LoginStep.AccessTokenLabel))
            {
                var accessToken = ScenarioContext.Get<AccessTokenOutput>(LoginStep.AccessTokenLabel);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.AccessToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}