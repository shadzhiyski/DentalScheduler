using DentalScheduler.Entities;
using DentalScheduler.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.UseCases.Tests.Utilities.DataProviders
{
    public interface IUserDbDataProvider
    {
        User ProvideAdmin(string userName, string password);

        (User, DentalWorker) ProvideDentist(string userName, string password);

        (User, Patient) ProvidePatient(string userName, string password);
    }
}