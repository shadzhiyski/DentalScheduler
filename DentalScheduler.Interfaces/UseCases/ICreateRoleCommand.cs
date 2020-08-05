using System.Threading.Tasks;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output.Common;

namespace DentalScheduler.Interfaces.UseCases
{
    public interface ICreateRoleCommand
    {
        Task<IResult<IMessageOutput>> CreateAsync(ICreateRoleInput roleInput);
    }
}