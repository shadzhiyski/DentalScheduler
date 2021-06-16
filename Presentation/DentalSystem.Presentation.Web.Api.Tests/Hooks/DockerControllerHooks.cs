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

namespace DentalSystem.Presentation.Web.Api.Tests.Hooks
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
                    "dentalsystemwebapi", "https://localhost:5555/index.html",//$"{confirmationUrl}",
                    continuation: (response, _) => response.Code != System.Net.HttpStatusCode.OK ? 5000 : 0
                )
                .Build()
                .Start();

            Thread.Sleep(5000);
        }

        [AfterTestRun]
        public static void DockerComposeDown()
        {
            _compositeService.Stop();
            _compositeService.Dispose();
        }

        [BeforeScenario]
        public void AddHttpContext()
        {
            var config = LoadConfig();
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(config["DentalSystem.Presentation.Web.Api:BaseAddress"])
            };

            _objectContainer.RegisterInstanceAs(httpClient);
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