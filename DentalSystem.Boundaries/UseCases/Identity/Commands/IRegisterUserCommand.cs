using System.Threading.Tasks;
using DentalSystem.Boundaries.UseCases.Identity.Dto.Input;
using DentalSystem.Boundaries.UseCases.Identity.Dto.Output;
using DentalSystem.Boundaries.UseCases.Common.Dto.Output;

namespace DentalSystem.Boundaries.UseCases.Identity.Commands
{
    public interface IRegisterUserCommand
    {
        Task<IResult<IAccessTokenOutput>> RegisterAsync(IRegisterUserInput userInput);
    }
}