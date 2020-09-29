using System.Collections.Generic;
using System.Linq;
using DentalScheduler.Dto.Output;
using DentalScheduler.Interfaces.Models.Output;
using DentalScheduler.Interfaces.Models.Output.Common;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.Config.Mappings
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