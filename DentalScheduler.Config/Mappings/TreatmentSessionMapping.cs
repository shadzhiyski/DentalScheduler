using DentalScheduler.DTO.Output;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Models.Output;
using Mapster;

namespace DentalScheduler.Config.Mappings
{
    public class TreatmentSessionMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TreatmentSession, ITreatmentSessionOutput>()
                .MapWith((src) => 
                new TreatmentSessionOutput
                {
                    ReferenceId = src.ReferenceId,
                    PatientReferenceId = src.Patient.ReferenceId,
                    DentalTeam = src.DentalTeam.Adapt<DentalTeamOutput>(config),
                    Treatment = src.DentalTeam.Adapt<TreatmentOutput>(config),
                    Start = src.Start,
                    End = src.End,
                    Status = src.Status.ToString()
                });
        }
    }
}