using System.Threading.Tasks;
using DentalScheduler.DTO.Input;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output;

namespace DentalScheduler.Web.UI.Services
{
    public interface IUserService
    {
        Task<IUserProfileOutput> GetProfile();

        Task SetProfile(IUserProfileInput input);
    }
}