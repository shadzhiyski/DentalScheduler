using DentalSystem.Application.UseCases.Scheduling.Dto.Input;
using DentalSystem.Application.UseCases.Scheduling.Dto.Output;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Output;
using Mapster;

namespace DentalSystem.Application.UseCases.Scheduling.Mappings
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
                    Patient = src.Patient.Adapt<PatientOutput>(config),
                    Status = src.Status.ToString()
                });

            config.NewConfig<ITreatmentSessionOutput, TreatmentSessionInput>()
                .MapWith((src) =>
                new TreatmentSessionInput
                {
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