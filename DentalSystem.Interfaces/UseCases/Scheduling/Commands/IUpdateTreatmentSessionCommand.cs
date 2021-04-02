using System.Threading.Tasks;
using DentalSystem.Interfaces.UseCases.Scheduling.Dto.Input;
using DentalSystem.Interfaces.UseCases.Common.Dto.Output;

namespace DentalSystem.Interfaces.UseCases.Scheduling.Commands
{
    public interface IUpdateTreatmentSessionCommand
    {
        Task<IResult<IMessageOutput>> ExecuteAsync(ITreatmentSessionInput input);
    }
}