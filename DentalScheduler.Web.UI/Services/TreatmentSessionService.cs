using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Web.UI.Models;
using Simple.OData.Client;

namespace DentalScheduler.Web.UI.Services
{
    public class TreatmentSessionService : ITreatmentSessionService
    {
        public TreatmentSessionService(
                ODataClient oDataClient,
                IHttpClientFactory httpClientFactory)
        {
            ODataClient = oDataClient;
            HttpClient = httpClientFactory.CreateClient("DataClient");
        }
        
        public ODataClient ODataClient { get; }

        public HttpClient HttpClient { get; }

        public async Task<IList<TreatmentSessionViewModel>> GetAppointmentsAsync(
            DateTimeOffset periodStart, DateTimeOffset periodEnd)
            => (await ODataClient
                    .For<TreatmentSessionViewModel>("TreatmentSession")
                    .Expand(m => m.DentalTeam)
                    .Expand(m => m.Treatment)
                    .Filter(m => m.Start <= periodEnd && m.End >= periodStart)
                    .FindEntriesAsync()
                )
                .ToList();

        public async Task AddAppointmentsAsync(ITreatmentSessionInput input)
            => await HttpClient.PostAsJsonAsync<ITreatmentSessionInput>("TreatmentSession", input);

        public async Task EditAppointmentsAsync(ITreatmentSessionInput input) 
            => await HttpClient.PutAsJsonAsync<ITreatmentSessionInput>("TreatmentSession", input);
    }
}