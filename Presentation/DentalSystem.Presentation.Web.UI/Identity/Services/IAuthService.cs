using System.Threading.Tasks;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Output;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;

namespace DentalSystem.Presentation.Web.UI.Identity.Services
{
    public interface IAuthService
    {
        Task<IResult<IAccessTokenOutput>> LoginAsync(IUserCredentialsInput input);

        Task<IResult<IAccessTokenOutput>> RegisterUserAsync(IUserCredentialsInput input);

        Task<IResult<IAccessTokenOutput>> RefreshTokenAsync(IUserCredentialsInput input);
    }
}