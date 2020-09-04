using DentalScheduler.DTO.Output;
using DentalScheduler.DTO.Serialization.Json;
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

            config.NewConfig<DeserializedTreatmentSession, ITreatmentSessionOutput>()
                .MapWith((src) => 
                new TreatmentSessionOutput
                {
                    PatientReferenceId = src.PatientReferenceId,
                    DentalTeam = src.DentalTeam.Adapt<IDentalTeamOutput>(config),
                    Start = src.Start,
                    End = src.End,
                    Reason = src.Reason
                });
        }
    }
}