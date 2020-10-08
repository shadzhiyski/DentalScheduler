using System.Threading.Tasks;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.Interfaces.UseCases.Identity.Commands
{
    public interface ILinkUserAndRoleCommand
    {
        Task<IResult<IMessageOutput>> ExecuteAsync(ILinkUserAndRoleInput userInput);
    }
}