using DentalScheduler.UseCases.Common.Dto.Output;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.UseCases.Common.Mappings
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