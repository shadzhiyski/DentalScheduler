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
                    PatientReferenceId = src.Patient.ReferenceId,
                    DentalTeam = src.DentalTeam.Adapt<IDentalTeamOutput>(config),
                    Start = src.Start,
                    End = src.End,
                    Reason = src.Reason
                });
        }
    }
}