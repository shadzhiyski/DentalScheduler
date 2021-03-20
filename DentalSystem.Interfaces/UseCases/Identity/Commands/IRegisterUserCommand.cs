using System.Threading.Tasks;
using DentalSystem.Interfaces.UseCases.Identity.Dto.Input;
using DentalSystem.Interfaces.UseCases.Identity.Dto.Output;
using DentalSystem.Interfaces.UseCases.Common.Dto.Output;

namespace DentalSystem.Interfaces.UseCases.Identity.Commands
{
    public interface IRegisterUserCommand
    {
        Task<IResult<IAccessTokenOutput>> RegisterAsync(IRegisterUserInput userInput);
    }
}