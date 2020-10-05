using System.Threading.Tasks;
using DentalScheduler.Interfaces.Dto.Input;
using DentalScheduler.Interfaces.Dto.Output;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.Interfaces.UseCases.Identity
{
    public interface ILoginCommand
    {
        Task<IResult<IAccessTokenOutput>> LoginAsync(IUserCredentialsInput userInput);
    }
}