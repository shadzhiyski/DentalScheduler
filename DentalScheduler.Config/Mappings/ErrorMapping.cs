using DentalScheduler.Dto.Output.Common;
using DentalScheduler.Interfaces.Models.Output.Common;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.Config.Mappings
{
    public class ErrorMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<IdentityError, IError>()
                .MapWith((src) => new Error(ErrorType.Validation, src.Description));
        }
    }
}