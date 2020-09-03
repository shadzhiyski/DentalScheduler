using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using Mapster;
using DentalScheduler.Interfaces.Models.Output;
using DentalScheduler.Interfaces.Models.Input;

namespace DentalScheduler.Web.UI.Services
{
    public class ScheduleService : IScheduleService
    {
        public ScheduleService(
            HttpClient httpClient, 
            IOptions<AppSettings> appSettings,
            ILocalStorageService localStorage)
        {
            HttpClient = httpClient;
            AppSettings = appSettings.Value;
            LocalStorage = localStorage;

            HttpClient.BaseAddress = new Uri($"{AppSettings.ApiBaseAddress}odata/");

            SetAccessTokenAsync();
        }

        public HttpClient HttpClient { get; }
        
        public AppSettings AppSettings { get; }

        public ILocalStorageService LocalStorage { get; }

        public async Task<IList<ITreatmentSessionOutput>> GetAppointmentsAsync(
            DateTimeOffset periodStart, DateTimeOffset periodEnd)
        {
            var response = await HttpClient.GetAsync("TreatmentSession");

            var result = (await response.Content.ReadFromJsonAsync<List<DTO.Serialization.Json.SerializedTreatmentSession>>())
                .Select(sts => sts.Adapt<ITreatmentSessionOutput>())
                .ToList();
            
            return result;
        }

        public async Task AddAppointmentsAsync(ITreatmentSessionInput input)
        {
            var response = await HttpClient.PostAsJsonAsync<ITreatmentSessionInput>("TreatmentSession", input);

            System.Console.WriteLine($"{response.StatusCode}");
        }

        public async Task EditAppointmentsAsync(ITreatmentSessionInput input)
        {
            var response = await HttpClient.PutAsJsonAsync<ITreatmentSessionInput>("TreatmentSession", input);

            System.Console.WriteLine($"{response.StatusCode}");
        }

        private async Task SetAccessTokenAsync()
        {
            var accessToken = await LocalStorage.GetItemAsync<string>("AccessToken");

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}