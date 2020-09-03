using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output;
using DentalScheduler.Web.UI.Models;
using Radzen.Blazor;

namespace DentalScheduler.Web.UI.Services
{
    public interface IScheduleService
    {
        Task<IList<ITreatmentSessionOutput>> GetAppointmentsAsync(
            DateTimeOffset periodStart, DateTimeOffset periodEnd);

        Task AddAppointmentsAsync(ITreatmentSessionInput input);

        Task EditAppointmentsAsync(ITreatmentSessionInput input);
    }
}