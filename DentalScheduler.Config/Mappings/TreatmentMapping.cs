using DentalScheduler.UseCases.Scheduling.Dto.Output;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.UseCases.Scheduling.Dto.Output;
using Mapster;

namespace DentalScheduler.Config.Mappings
{
    public class TreatmentMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Treatment, ITreatmentOutput>()
                .MapWith((src) => new TreatmentOutput()
                {
                    ReferenceId = src.ReferenceId,
                    Name = src.Name,
                    DurationInMinutes = src.DurationInMinutes
                });
        }
    }
}