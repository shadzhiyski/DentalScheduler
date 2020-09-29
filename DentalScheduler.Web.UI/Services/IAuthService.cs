using System.Threading.Tasks;
using DentalScheduler.Interfaces.Dto.Input;
using DentalScheduler.Interfaces.Dto.Output;
using DentalScheduler.Interfaces.Dto.Output.Common;

namespace DentalScheduler.Web.UI.Services
{
    public interface IAuthService
    {
        Task<IResult<IAccessTokenOutput>> LoginAsync(IUserCredentialsInput input);
        
        Task<IResult<IAccessTokenOutput>> RegisterUserAsync(IUserCredentialsInput input);

        Task<IResult<IAccessTokenOutput>> RefreshTokenAsync(IUserCredentialsInput input);
    }
}