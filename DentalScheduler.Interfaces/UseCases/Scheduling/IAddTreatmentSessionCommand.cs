using System.Threading.Tasks;
using DentalScheduler.Interfaces.UseCases.Scheduling.Dto.Input;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.Interfaces.UseCases.Scheduling
{
    public interface IAddTreatmentSessionCommand
    {
        Task<IResult<IMessageOutput>> ExecuteAsync(ITreatmentSessionInput input);
    }
}