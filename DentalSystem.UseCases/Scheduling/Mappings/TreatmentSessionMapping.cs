using DentalSystem.UseCases.Scheduling.Dto.Input;
using DentalSystem.UseCases.Scheduling.Dto.Output;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Interfaces.UseCases.Scheduling.Dto.Output;
using Mapster;

namespace DentalSystem.UseCases.Scheduling.Mappings
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
                    Treatment = src.Treatment.Adapt<TreatmentOutput>(config),
                    Start = src.Start,
                    End = src.End,
                    Status = src.Status.ToString()
                });

            config.NewConfig<ITreatmentSessionOutput, TreatmentSessionInput>()
                .MapWith((src) =>
                new TreatmentSessionInput
                {
                    ReferenceId = src.ReferenceId,
                    PatientReferenceId = src.PatientReferenceId,
                    DentalTeamReferenceId = src.DentalTeam.ReferenceId,
                    TreatmentReferenceId = src.Treatment.ReferenceId,
                    Start = src.Start,
                    End = src.End,
                    Status = src.Status.ToString()
                });
        }
    }
}