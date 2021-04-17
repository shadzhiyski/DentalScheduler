using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Scheduling.Dto.Output;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input;
using DentalSystem.Presentation.Web.UI.Models;

namespace DentalSystem.Presentation.Web.UI.Scheduling.Services
{
    public interface ITreatmentSessionService
    {
        Task<List<TreatmentSessionViewModel>> GetAppointmentsAsync(
            DateTimeOffset periodStart, DateTimeOffset periodEnd);

        Task<List<TreatmentSessionOutput>> GetAppointmentsHistoryAsync(
            Guid patientReferenceId,
            int pageIndex,
            int pageSize);

        Task<TreatmentSessionOutput> GetAppointment(Guid referenceId, Guid patientReferenceId);

        Task AddAppointmentsAsync(ITreatmentSessionInput input);

        Task EditAppointmentsAsync(ITreatmentSessionInput input);
    }
}