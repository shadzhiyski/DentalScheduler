using System.Threading.Tasks;
using DentalSystem.Boundaries.UseCases.Scheduling.Dto.Input;
using DentalSystem.Boundaries.UseCases.Common.Dto.Output;

namespace DentalSystem.Boundaries.UseCases.Scheduling.Commands
{
    public interface IAddTreatmentSessionCommand
    {
        Task<IResult<IMessageOutput>> ExecuteAsync(ITreatmentSessionInput input);
    }
}