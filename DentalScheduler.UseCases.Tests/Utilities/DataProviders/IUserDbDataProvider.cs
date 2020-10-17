using System.Collections.Generic;
using System.Threading.Tasks;
using DentalScheduler.Entities;
using DentalScheduler.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.UseCases.Tests.Utilities.DataProviders
{
    public interface IUserDbDataProvider
    {
        Task<User> ProvideAdminAsync(string userName, string password);

        Task<(User, DentalWorker)> ProvideDentistAsync(string userName, string password);

        Task<(User, Patient)> ProvidePatientAsync(string userName, string password);

        Task<IEnumerable<IdentityRole>> ProvideRolesAsync(params string[] roles);
    }
}