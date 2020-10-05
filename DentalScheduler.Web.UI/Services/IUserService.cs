using System.Threading.Tasks;
using DentalScheduler.UseCases.Scheduling.Dto.Input;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Output;

namespace DentalScheduler.Web.UI.Services
{
    public interface IUserService
    {
        Task<byte[]> GetAvatar();

        Task<IUserProfileOutput> GetProfile();

        Task SetProfile(IUserProfileInput input);
    }
}