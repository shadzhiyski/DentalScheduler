using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Web.UI.Models;

namespace DentalScheduler.Web.UI.Services
{
    public interface IScheduleService
    {
        Task<IList<TreatmentSessionViewModel>> GetAppointmentsAsync(
            DateTimeOffset periodStart, DateTimeOffset periodEnd);

        Task AddAppointmentsAsync(ITreatmentSessionInput input);

        Task EditAppointmentsAsync(ITreatmentSessionInput input);
    }
}