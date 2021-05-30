using System.Threading.Tasks;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Output;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;
using System.Threading;

namespace DentalSystem.Application.Boundaries.UseCases.Identity.Commands
{
    public interface ILoginCommand
    {
        Task<IResult<IAccessTokenOutput>> LoginAsync(IUserCredentialsInput userInput, CancellationToken cancellationToken);
    }
}