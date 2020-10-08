using System.Threading.Tasks;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.Interfaces.UseCases.Identity.Commands
{
    public interface ICreateRoleCommand
    {
        Task<IResult<IMessageOutput>> CreateAsync(ICreateRoleInput roleInput);
    }
}