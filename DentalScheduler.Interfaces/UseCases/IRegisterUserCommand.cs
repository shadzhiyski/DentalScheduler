using System.Threading.Tasks;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output;
using DentalScheduler.Interfaces.Models.Output.Common;

namespace DentalScheduler.Interfaces.UseCases
{
    public interface IRegisterUserCommand
    {
        Task<IResult<IAccessTokenOutput>> RegisterAsync(IRegisterUserInput userInput);
    }
}