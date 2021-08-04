using DentalSystem.Application.UseCases.Scheduling.Dto.Output;
using DentalSystem.Domain.Scheduling;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Output;
using Mapster;

namespace DentalSystem.Application.UseCases.Scheduling.Mappings
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