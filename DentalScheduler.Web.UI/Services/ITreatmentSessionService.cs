using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalScheduler.UseCases.Scheduling.Dto.Output;
using DentalScheduler.Interfaces.UseCases.Scheduling.Dto.Input;
using DentalScheduler.Web.UI.Models;

namespace DentalScheduler.Web.UI.Services
{
    public interface ITreatmentSessionService
    {
        Task<List<TreatmentSessionViewModel>> GetAppointmentsAsync(
            DateTimeOffset periodStart, DateTimeOffset periodEnd);
        
        Task<TreatmentSessionOutput> GetAppointment(Guid referenceId, Guid patientReferenceId);

        Task AddAppointmentsAsync(ITreatmentSessionInput input);

        Task EditAppointmentsAsync(ITreatmentSessionInput input);
    }
}