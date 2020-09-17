using DentalScheduler.Entities;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.UseCases.Tests.Utilities.DataProviders
{
    public interface IUserDbDataProvider
    {
        IdentityUser ProvideAdmin(string userName, string password);

        (IdentityUser, DentalWorker) ProvideDentist(string userName, string password);

        (IdentityUser, Patient) ProvidePatient(string userName, string password);
    }
}