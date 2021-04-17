using System.Threading.Tasks;
using DentalSystem.Boundaries.UseCases.Identity.Dto.Input;
using DentalSystem.Boundaries.UseCases.Identity.Dto.Output;
using DentalSystem.Boundaries.UseCases.Common.Dto.Output;

namespace DentalSystem.Web.UI.Identity.Services
{
    public interface IAuthService
    {
        Task<IResult<IAccessTokenOutput>> LoginAsync(IUserCredentialsInput input);

        Task<IResult<IAccessTokenOutput>> RegisterUserAsync(IUserCredentialsInput input);

        Task<IResult<IAccessTokenOutput>> RefreshTokenAsync(IUserCredentialsInput input);
    }
}