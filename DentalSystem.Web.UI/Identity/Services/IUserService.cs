using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Scheduling.Dto.Input;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Output;

namespace DentalSystem.Web.UI.Identity.Services
{
    public interface IUserService
    {
        Task<byte[]> GetAvatar();

        Task<IUserProfileOutput> GetProfile();

        Task SetProfile(IUserProfileInput input);
    }
}