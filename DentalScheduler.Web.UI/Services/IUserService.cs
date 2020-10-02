using System.Threading.Tasks;
using DentalScheduler.Dto.Input;
using DentalScheduler.Interfaces.Dto.Input;
using DentalScheduler.Interfaces.Dto.Output;

namespace DentalScheduler.Web.UI.Services
{
    public interface IUserService
    {
        Task<byte[]> GetAvatar();

        Task<IUserProfileOutput> GetProfile();

        Task SetProfile(IUserProfileInput input);
    }
}