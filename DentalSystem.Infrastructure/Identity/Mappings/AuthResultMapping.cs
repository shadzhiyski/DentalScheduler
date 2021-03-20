using System.Linq;
using DentalSystem.Infrastructure.Identity.Dto.Output;
using DentalSystem.Interfaces.Infrastructure.Identity.Dto.Output;
using DentalSystem.Interfaces.UseCases.Common.Dto.Output;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace DentalSystem.Infrastructure.Identity.Mappings
{
    public class AuthResultMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<IdentityResult, IAuthResult>()
                .MapWith((src) =>
                    new AuthResult(
                        src.Succeeded,
                        src.Errors.Select(e => e.Adapt<IError>(config))
                    )
                );
        }
    }
}