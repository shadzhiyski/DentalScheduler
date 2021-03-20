using System.Threading.Tasks;
using DentalSystem.Interfaces.UseCases.Identity.Dto.Input;
using DentalSystem.Interfaces.UseCases.Common.Dto.Output;

namespace DentalSystem.Interfaces.UseCases.Identity.Commands
{
    public interface ICreateRoleCommand
    {
        Task<IResult<IMessageOutput>> CreateAsync(ICreateRoleInput roleInput);
    }
}