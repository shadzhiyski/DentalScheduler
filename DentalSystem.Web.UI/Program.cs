using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using DentalSystem.Web.UI.Identity.Services;
using DentalSystem.Web.UI.Scheduling.Services;
using DentalSystem.Web.UI.Common.Services;
using Radzen;
using DentalSystem.Web.UI.Common.Handlers;
using System;
using Microsoft.Extensions.Options;
using Simple.OData.Client;
using DentalSystem.Application.UseCases;

namespace DentalSystem.Web.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Simple.OData.Client.V4Adapter.Reference();

            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            var appSettingSection = builder.Configuration.GetSection("AppSettings");
            builder.Services.Configure<AppSettings>(
                (appSettings) => appSettings.ApiBaseAddress = appSettingSection.GetSection("ApiBaseAddress").Value
            );

            RegisterServices(builder.Services);

            RegisterHandlers(builder.Services);

            builder.Services
                .AddLightUseCases();

            builder.Services.AddLocalization();

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<AuthenticationStateProvider, ApplicationAuthenticationStateProvider>();
            builder.Services.AddScoped<ApplicationAuthenticationStateProvider, ApplicationAuthenticationStateProvider>();

            var seriveProvider = builder.Services.BuildServiceProvider();
            builder.Services.AddHttpClient("AuthClient", client =>
                {
                    var appSettings = seriveProvider.GetRequiredService<IOptions<AppSettings>>();
                    client.BaseAddress = new Uri($"{appSettings.Value.ApiBaseAddress}/");
                })
                .AddHttpMessageHandler<BlazorDisplaySpinnerAutomaticallyHttpMessageHandler>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler());

            builder.Services.AddHttpClient("UserClient", client =>
                {
                    var appSettings = seriveProvider.GetRequiredService<IOptions<AppSettings>>();
                    client.BaseAddress = new Uri($"{appSettings.Value.ApiBaseAddress}/");
                })
                .AddHttpMessageHandler<AuthorizationHeaderHttpHandler>()
                .AddHttpMessageHandler<BlazorDisplaySpinnerAutomaticallyHttpMessageHandler>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler());

            builder.Services.AddHttpClient("DataClient", client =>
                {
                    var appSettings = seriveProvider.GetRequiredService<IOptions<AppSettings>>();
                    client.BaseAddress = new Uri($"{appSettings.Value.ApiBaseAddress}/odata/");
                })
                .AddHttpMessageHandler<AuthorizationHeaderHttpHandler>()
                .AddHttpMessageHandler<BlazorDisplaySpinnerAutomaticallyHttpMessageHandler>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler());
            builder.Services.AddScoped<ODataClient>(sp =>
                {
                    var httpClient = sp.GetRequiredService<IHttpClientFactory>()
                        .CreateClient("DataClient");
                    var clientSettings = new ODataClientSettings(httpClient);

                    return new ODataClient(clientSettings);
                });

            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();

            builder.RootComponents.Add<App>("app");

            var host = builder.Build();

            var localizationService = host.Services.GetRequiredService<ILocalizationService>();
            await localizationService.SetDefaultCultureAsync();

            await host.RunAsync();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITreatmentSessionService, TreatmentSessionService>();
            services.AddTransient<IDentalTeamService, DentalTeamService>();
            services.AddTransient<ITreatmentService, TreatmentService>();
            services.AddTransient<IUserService, UserService>();

            services.AddHttpClient<IAuthService, AuthService>("AuthClient");
            services.AddHttpClient<IUserService, UserService>("UserClient");
            services.AddHttpClient<ITreatmentSessionService, TreatmentSessionService>("DataClient");

            services.AddSingleton<ISpinnerService, SpinnerService>();
            services.AddTransient<ILocalizationService, LocalizationService>();
        }

        private static void RegisterHandlers(IServiceCollection services)
        {
            services.AddTransient<AuthorizationHeaderHttpHandler>();
            services.AddTransient<BlazorDisplaySpinnerAutomaticallyHttpMessageHandler>();
        }
    }
}
