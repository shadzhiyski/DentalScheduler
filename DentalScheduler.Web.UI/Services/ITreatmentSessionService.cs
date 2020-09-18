using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalScheduler.DTO.Output;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Web.UI.Models;

namespace DentalScheduler.Web.UI.Services
{
    public interface ITreatmentSessionService
    {
        Task<IList<TreatmentSessionViewModel>> GetAppointmentsAsync(
            DateTimeOffset periodStart, DateTimeOffset periodEnd);
        
        Task<TreatmentSessionOutput> GetAppointment(Guid referenceId, Guid patientReferenceId);

        Task AddAppointmentsAsync(ITreatmentSessionInput input);

        Task EditAppointmentsAsync(ITreatmentSessionInput input);
    }
}