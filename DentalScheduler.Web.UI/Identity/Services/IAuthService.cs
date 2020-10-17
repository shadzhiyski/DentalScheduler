using System.Threading.Tasks;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Output;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.Web.UI.Identity.Services
{
    public interface IAuthService
    {
        Task<IResult<IAccessTokenOutput>> LoginAsync(IUserCredentialsInput input);

        Task<IResult<IAccessTokenOutput>> RegisterUserAsync(IUserCredentialsInput input);

        Task<IResult<IAccessTokenOutput>> RefreshTokenAsync(IUserCredentialsInput input);
    }
}