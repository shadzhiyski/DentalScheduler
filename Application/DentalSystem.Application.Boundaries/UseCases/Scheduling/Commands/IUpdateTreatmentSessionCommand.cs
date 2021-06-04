using System.Threading.Tasks;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;
using System.Threading;

namespace DentalSystem.Application.Boundaries.UseCases.Scheduling.Commands
{
    public interface IUpdateTreatmentSessionCommand
    {
        Task<IResult<IMessageOutput>> ExecuteAsync(IUpdateTreatmentSessionInput input, CancellationToken cancellationToken);
    }
}