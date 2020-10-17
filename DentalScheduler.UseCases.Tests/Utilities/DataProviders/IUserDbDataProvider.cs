using System.Collections.Generic;
using System.Threading.Tasks;
using DentalScheduler.Entities;
using DentalScheduler.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.UseCases.Tests.Utilities.DataProviders
{
    public interface IUserDbDataProvider
    {
        Task<User> ProvideAdmin(string userName, string password);

        Task<(User, DentalWorker)> ProvideDentist(string userName, string password);

        Task<(User, Patient)> ProvidePatient(string userName, string password);

        Task<IEnumerable<IdentityRole>> ProvideRoles(params string[] roles);
    }
}