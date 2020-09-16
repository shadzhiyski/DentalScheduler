using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DentalScheduler.Config.DI;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using DentalScheduler.Web.UI.Services;
using Radzen;
using DentalScheduler.Web.UI.Handlers;
using System;
using Microsoft.Extensions.Options;
using Simple.OData.Client;

namespace DentalScheduler.Web.UI
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

            builder.Services.RegisterDependencies();

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

            await builder.Build().RunAsync();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITreatmentSessionService, TreatmentSessionService>();
            services.AddTransient<IDentalTeamService, DentalTeamService>();
            services.AddTransient<ITreatmentService, TreatmentService>();
            
            services.AddScoped<ISpinnerService, SpinnerService>();
        }

        private static void RegisterHandlers(IServiceCollection services)
        {
            services.AddTransient<AuthorizationHeaderHttpHandler>();
            services.AddTransient<BlazorDisplaySpinnerAutomaticallyHttpMessageHandler>();
        }
    }
}
