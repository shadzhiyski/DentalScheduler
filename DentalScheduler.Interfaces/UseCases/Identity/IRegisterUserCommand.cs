using System.Threading.Tasks;
using DentalScheduler.Interfaces.Dto.Input;
using DentalScheduler.Interfaces.Dto.Output;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.Interfaces.UseCases.Identity
{
    public interface IRegisterUserCommand
    {
        Task<IResult<IAccessTokenOutput>> RegisterAsync(IRegisterUserInput userInput);
    }
}