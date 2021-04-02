using System.Collections.Generic;
using System.Threading.Tasks;
using DentalSystem.Entities;
using DentalSystem.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace DentalSystem.UseCases.Tests.Utilities.DataProviders
{
    public interface IUserDbDataProvider
    {
        Task<User> ProvideAdminAsync(string userName, string password);

        Task<(User, DentalWorker)> ProvideDentistAsync(string userName, string password);

        Task<(User, Patient)> ProvidePatientAsync(string userName, string password);

        Task<IEnumerable<IdentityRole>> ProvideRolesAsync(params string[] roles);
    }
}