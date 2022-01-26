using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using BoDi;
using System.Net.Http;
using System;
using System.Threading;
using DentalSystem.Presentation.Web.Api.Tests.Common.Steps;
using Simple.OData.Client;
using DentalSystem.Presentation.Web.Api.Tests.Common.Handlers;

namespace DentalSystem.Presentation.Web.Api.Tests.Common.Hooks
{
    [Binding]
    public class DockerControllerHooks
    {
        private static ICompositeService _compositeService;
        private IObjectContainer _objectContainer;

        public DockerControllerHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void DockerComposeUp()
        {
            var config = LoadConfig();

            var dockerComposeFileName = config["DockerComposeFileName"];
            var dockerComposePath = GetDockerComposeLocation(dockerComposeFileName);

            var confirmationUrl = config["DentalSystem.Presentation.Web.Api:BaseAddress"];

            _compositeService = new Builder()
                .UseContainer()
                .UseCompose()
                .FromFile(dockerComposePath)
                .RemoveOrphans()
                .WaitForHttp(
                    "dentalsystemtestwebapi", $"{confirmationUrl}/index.html",
                    continuation: (response, _) => response.Code != System.Net.HttpStatusCode.OK ? 100 : 0
                )
                .Build()
                .Start();
        }

        [AfterTestRun]
        public static void DockerComposeDown()
        {
            _compositeService.Stop();
            _compositeService.Dispose();
        }

        [BeforeScenario]
        public void AddScenarioPrerequisites()
        {
            _objectContainer.RegisterTypeAs<AuthorizationHeaderHttpHandler, AuthorizationHeaderHttpHandler>();

            var config = LoadConfig();
            var httpClient = new HttpClient(handler: _objectContainer.Resolve<AuthorizationHeaderHttpHandler>())
            {
                BaseAddress = new Uri(config["DentalSystem.Presentation.Web.Api:BaseAddress"])
            };

            _objectContainer.RegisterFactoryAs<HttpClient>((objContainer) => new HttpClient(handler: objContainer.Resolve<AuthorizationHeaderHttpHandler>())
            {
                BaseAddress = new Uri(config["DentalSystem.Presentation.Web.Api:BaseAddress"])
            });


            _objectContainer.RegisterFactoryAs<ODataClient>((objContainer) =>
            {
                var oDataHttpClient = new HttpClient(handler: objContainer.Resolve<AuthorizationHeaderHttpHandler>())
                {
                    BaseAddress = new Uri($"{config["DentalSystem.Presentation.Web.Api:BaseAddress"]}/odata")
                };

                var oDataClientSettings = new ODataClientSettings(oDataHttpClient);
                return new ODataClient(oDataClientSettings);
            });

            _objectContainer.RegisterTypeAs<LoginStep, LoginStep>();
            _objectContainer.RegisterTypeAs<RegisterUserStep, RegisterUserStep>();
            _objectContainer.RegisterTypeAs<ShouldReceiveAccessTokenStep, ShouldReceiveAccessTokenStep>();
            _objectContainer.RegisterTypeAs<ShouldReceiveLoginErrorsStep, ShouldReceiveLoginErrorsStep>();
        }

        private static IConfiguration LoadConfig()
            => new ConfigurationBuilder()
                .AddJsonFile("testsettings.json")
                .Build();

        private static string GetDockerComposeLocation(string dockerComposeFileName)
        {
            var directory = Directory.GetCurrentDirectory();
            while (!Directory.EnumerateFiles(directory, "*.yml")?.Any(fn => fn?.EndsWith(dockerComposeFileName) ?? false) ?? false)
            {
                directory = directory.Substring(0, directory.LastIndexOf(Path.DirectorySeparatorChar));
            }

            return Path.Combine(directory, dockerComposeFileName);
        }
    }
}