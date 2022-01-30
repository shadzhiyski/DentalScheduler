using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input;
using DentalSystem.Presentation.Web.UI.Models;
using Simple.OData.Client;
using DentalSystem.Application.UseCases.Scheduling.Dto.Output;

namespace DentalSystem.Presentation.Web.UI.Scheduling.Services
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
                    .Expand(m => m.Treatment)
                    .Select(m => new
                    {
                        m.ReferenceId,
                        m.PatientReferenceId,
                        m.Status,
                        m.Start,
                        m.End,
                        Treatment = new
                        {
                            m.Treatment.Name
                        }
                    })
                    .Filter(m => m.Start <= periodEnd && m.End >= periodStart)
                    .FindEntriesAsync()
                )
                .ToList();

        public async Task<List<TreatmentSessionOutput>> GetAppointmentsHistoryAsync(
            Guid patientReferenceId,
            int pageIndex,
            int pageSize)
            => (await ODataClient
                    .For<TreatmentSessionOutput>("TreatmentSession")
                    .Expand(m => m.Treatment)
                    .Expand(m => m.DentalTeam)
                    .Expand(m => m.Patient)
                    .Select(m => new
                    {
                        m.ReferenceId,
                        m.PatientReferenceId,
                        m.Status,
                        m.Start,
                        m.End,
                        Treatment = new
                        {
                            m.Treatment.Name
                        },
                        DentalTeam = new
                        {
                            m.DentalTeam.Name
                        },
                        Patient = new
                        {
                            FirstName = m.Patient.FirstName,
                            LastName = m.Patient.LastName
                        }
                    })
                    .Filter(m => m.PatientReferenceId == patientReferenceId)
                    .OrderByDescending(m => m.Start)
                    .Skip(pageIndex * pageSize)
                    .Top(pageSize)
                    .FindEntriesAsync()
                )
                .ToList();

        public Task<TreatmentSessionOutput> GetAppointment(Guid referenceId, Guid patientReferenceId)
            => ODataClient
                .For<TreatmentSessionOutput>("TreatmentSession")
                .Expand(m => m.DentalTeam)
                .Expand(m => m.Treatment)
                .Expand(m => m.Patient)
                .Filter(
                    m => m.ReferenceId == referenceId
                        && m.PatientReferenceId == patientReferenceId
                )
                .FindEntryAsync();

        public Task AddAppointmentsAsync(ITreatmentSessionInput input)
            => HttpClient.PostAsJsonAsync<object>("TreatmentSession", input);

        public Task EditAppointmentsAsync(ITreatmentSessionInput input)
            => HttpClient.PutAsJsonAsync<object>("TreatmentSession", input);
    }
}