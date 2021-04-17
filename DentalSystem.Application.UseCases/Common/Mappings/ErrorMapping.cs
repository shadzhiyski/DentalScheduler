using DentalSystem.Application.UseCases.Common.Dto.Output;
using DentalSystem.Boundaries.UseCases.Common.Dto.Output;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace DentalSystem.Application.UseCases.Common.Mappings
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