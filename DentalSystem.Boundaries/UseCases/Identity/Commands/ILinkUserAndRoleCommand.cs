using System.Threading.Tasks;
using DentalSystem.Boundaries.UseCases.Identity.Dto.Input;
using DentalSystem.Boundaries.UseCases.Common.Dto.Output;

namespace DentalSystem.Boundaries.UseCases.Identity.Commands
{
    public interface ILinkUserAndRoleCommand
    {
        Task<IResult<IMessageOutput>> ExecuteAsync(ILinkUserAndRoleInput userInput);
    }
}