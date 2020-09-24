using System.Threading.Tasks;
using DentalScheduler.Interfaces.Models.Output;

namespace DentalScheduler.Interfaces.UseCases.Identity
{
    public interface IGetUserProfileQuery
    {
        Task<IUserProfileOutput> ExecuteAsync(string userName);
    }
}