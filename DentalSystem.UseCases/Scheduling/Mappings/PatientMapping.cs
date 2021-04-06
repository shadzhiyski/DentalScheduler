using Mapster;
using DentalSystem.UseCases.Scheduling.Dto.Output;
using DentalSystem.Entities.Scheduling;

namespace DentalSystem.UseCases.Scheduling.Mappings
{
    public class PatientMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Patient, PatientOutput>()
                .MapWith((src) => new PatientOutput()
                {
                    ReferenceId = src.ReferenceId,
                    Avatar = src.IdentityUser.Avatar,
                    FirstName = src.IdentityUser.FirstName,
                    LastName = src.IdentityUser.LastName
                });
        }
    }
}