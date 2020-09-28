using System.IO;
using System.Threading.Tasks;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output.Common;

namespace DentalScheduler.Interfaces.UseCases.Identity
{
    public interface IUpdateProfileCommand
    {
        Task<IResult<IMessageOutput>> ExecuteAsync(IProfileInfoInput input);
    }
}