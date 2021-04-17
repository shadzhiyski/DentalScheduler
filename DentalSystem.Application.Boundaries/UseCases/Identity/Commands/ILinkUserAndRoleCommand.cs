using System.Threading.Tasks;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;

namespace DentalSystem.Application.Boundaries.UseCases.Identity.Commands
{
    public interface ILinkUserAndRoleCommand
    {
        Task<IResult<IMessageOutput>> ExecuteAsync(ILinkUserAndRoleInput userInput);
    }
}