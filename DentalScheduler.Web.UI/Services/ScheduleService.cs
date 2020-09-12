using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Web.UI.Models;
using Simple.OData.Client;

namespace DentalScheduler.Web.UI.Services
{
    public class ScheduleService : BaseDataService, IScheduleService
    {
        public ScheduleService(
                HttpClient httpClient, 
                IOptions<AppSettings> appSettings,
                ILocalStorageService localStorage)
            : base(httpClient, localStorage)
        {
            AppSettings = appSettings.Value;

            HttpClient.BaseAddress = new Uri($"{AppSettings.ApiBaseAddress}odata/");
        }
        
        public AppSettings AppSettings { get; }

        public async Task<IList<TreatmentSessionViewModel>> GetAppointmentsAsync(
            DateTimeOffset periodStart, DateTimeOffset periodEnd)
        {
            await SetAccessTokenAsync();

            var result = await ODataClient
                .For<TreatmentSessionViewModel>("TreatmentSession")
                .Expand(m => m.DentalTeam)
                .Expand(m => m.Treatment)
                .Filter(m => m.Start <= periodEnd && m.End >= periodStart)
                .FindEntriesAsync();
            
            return result.ToList();
        }

        public async Task AddAppointmentsAsync(ITreatmentSessionInput input)
        {
            await SetAccessTokenAsync();

            var response = await HttpClient.PostAsJsonAsync<ITreatmentSessionInput>("TreatmentSession", input);

            System.Console.WriteLine("{response.StatusCode}");
        }

        public async Task EditAppointmentsAsync(ITreatmentSessionInput input)
        {
            await SetAccessTokenAsync();

            var response = await HttpClient.PutAsJsonAsync<ITreatmentSessionInput>("TreatmentSession", input);

            System.Console.WriteLine("{response.StatusCode}");
        }
    }
}