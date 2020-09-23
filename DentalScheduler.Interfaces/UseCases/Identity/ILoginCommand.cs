using System.Threading.Tasks;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output;
using DentalScheduler.Interfaces.Models.Output.Common;

namespace DentalScheduler.Interfaces.UseCases.Identity
{
    public interface ILoginCommand
    {
        Task<IResult<IAccessTokenOutput>> LoginAsync(IUserCredentialsInput userInput);
    }
}