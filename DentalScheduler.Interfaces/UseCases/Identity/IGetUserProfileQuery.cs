using System.Threading.Tasks;
using DentalScheduler.Interfaces.Dto.Output;

namespace DentalScheduler.Interfaces.UseCases.Identity
{
    public interface IGetUserProfileQuery
    {
        Task<IUserProfileOutput> ExecuteAsync();
    }
}