using System.Threading.Tasks;
using DentalScheduler.Interfaces.Dto.Input;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.Interfaces.UseCases.Identity
{
    public interface ICreateRoleCommand
    {
        Task<IResult<IMessageOutput>> CreateAsync(ICreateRoleInput roleInput);
    }
}