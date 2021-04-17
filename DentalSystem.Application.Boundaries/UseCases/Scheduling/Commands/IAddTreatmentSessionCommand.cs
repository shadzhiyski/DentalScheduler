using System.Threading.Tasks;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;

namespace DentalSystem.Application.Boundaries.UseCases.Scheduling.Commands
{
    public interface IAddTreatmentSessionCommand
    {
        Task<IResult<IMessageOutput>> ExecuteAsync(ITreatmentSessionInput input);
    }
}