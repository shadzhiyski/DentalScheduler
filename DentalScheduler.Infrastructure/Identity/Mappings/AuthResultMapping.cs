using System.Linq;
using DentalScheduler.Infrastructure.Identity.Dto.Output;
using DentalScheduler.Interfaces.Infrastructure.Identity.Dto.Output;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.Infrastructure.Identity.Mappings
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