using System.Threading.Tasks;
using DentalScheduler.Interfaces.Dto.Input;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.Interfaces.UseCases.Identity
{
    public interface ILinkUserAndRoleCommand
    {
        Task<IResult<IMessageOutput>> ExecuteAsync(ILinkUserAndRoleInput userInput);
    }
}