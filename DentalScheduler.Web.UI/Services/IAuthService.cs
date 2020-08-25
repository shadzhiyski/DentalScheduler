using System.Threading.Tasks;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output;
using DentalScheduler.Interfaces.Models.Output.Common;

namespace DentalScheduler.Web.UI.Services
{
    public interface IAuthService
    {
        Task<IResult<IAccessTokenOutput>> LoginAsync(IUserCredentialsInput input);
        
        Task<IResult<IAccessTokenOutput>> RegisterUserAsync(IUserCredentialsInput input);

        Task<IResult<IAccessTokenOutput>> RefreshTokenAsync(IUserCredentialsInput input);
    }
}