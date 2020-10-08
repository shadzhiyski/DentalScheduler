using System.Threading.Tasks;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Output;

namespace DentalScheduler.Interfaces.UseCases.Identity.Queries
{
    public interface IGetUserProfileQuery
    {
        Task<IUserProfileOutput> ExecuteAsync();
    }
}