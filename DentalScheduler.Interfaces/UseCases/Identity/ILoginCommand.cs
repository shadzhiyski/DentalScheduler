using System.Threading.Tasks;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Output;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.Interfaces.UseCases.Identity
{
    public interface ILoginCommand
    {
        Task<IResult<IAccessTokenOutput>> LoginAsync(IUserCredentialsInput userInput);
    }
}