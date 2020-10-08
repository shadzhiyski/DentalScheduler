using System.IO;
using System.Threading.Tasks;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.Interfaces.UseCases.Identity.Commands
{
    public interface IUpdateProfileCommand
    {
        Task<IResult<IMessageOutput>> ExecuteAsync(IUserProfileInput input);
    }
}