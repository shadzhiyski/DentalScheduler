using System.Threading.Tasks;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output.Common;

namespace DentalScheduler.Interfaces.UseCases.TreatmentSessions
{
    public interface IAddTreatmentSessionCommand
    {
        Task<IResult<IMessageOutput>> ExecuteAsync(ITreatmentSessionInput input);
    }
}