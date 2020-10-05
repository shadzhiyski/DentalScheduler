using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using DentalScheduler.Interfaces.UseCases.Scheduling.Dto.Input;
using DentalScheduler.Web.UI.Models;
using Simple.OData.Client;
using DentalScheduler.UseCases.Scheduling.Dto.Output;

namespace DentalScheduler.Web.UI.Services
{
    public class TreatmentSessionService : ITreatmentSessionService
    {
        public TreatmentSessionService(
                ODataClient oDataClient,
                HttpClient httpClient)
        {
            ODataClient = oDataClient;
            HttpClient = httpClient;
        }
        
        public ODataClient ODataClient { get; }

        public HttpClient HttpClient { get; }

        public async Task<List<TreatmentSessionViewModel>> GetAppointmentsAsync(
            DateTimeOffset periodStart, DateTimeOffset periodEnd)
            => (await ODataClient
                    .For<TreatmentSessionViewModel>("TreatmentSession")
                    .Expand(m => m.DentalTeam)
                    .Expand(m => m.Treatment)
                    .Filter(m => m.Start <= periodEnd && m.End >= periodStart)
                    .FindEntriesAsync()
                )
                .ToList();

        public async Task<TreatmentSessionOutput> GetAppointment(Guid referenceId, Guid patientReferenceId)
            => await ODataClient
                .For<TreatmentSessionOutput>("TreatmentSession")
                .Expand(m => m.DentalTeam)
                .Expand(m => m.Treatment)
                .Filter(
                    m => m.ReferenceId == referenceId
                        && m.PatientReferenceId == patientReferenceId
                )
                .FindEntryAsync();

        public async Task AddAppointmentsAsync(ITreatmentSessionInput input)
            => await HttpClient.PostAsJsonAsync<ITreatmentSessionInput>("TreatmentSession", input);

        public async Task EditAppointmentsAsync(ITreatmentSessionInput input) 
            => await HttpClient.PutAsJsonAsync<ITreatmentSessionInput>("TreatmentSession", input);
    }
}