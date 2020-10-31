using Mapster;
using DentalScheduler.UseCases.Scheduling.Dto.Output;
using DentalScheduler.Entities;

namespace DentalScheduler.UseCases.Scheduling.Config.Mappings
{
    public class PatientMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Patient, PatientOutput>()
                .Map(dest => dest.Avatar, src => src.IdentityUser.Avatar)
                .Map(dest => dest.FirstName, src => src.IdentityUser.FirstName)
                .Map(dest => dest.LastName, src => src.IdentityUser.LastName);
        }
    }
}