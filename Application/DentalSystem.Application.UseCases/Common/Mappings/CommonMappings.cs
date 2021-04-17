using System;
using DentalSystem.Common.Helpers.Extensions;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace DentalSystem.Application.UseCases.Common.Mappings
{
    public class CommonMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<DateTimeOffset, DateTime>()
                .MapWith(src => src.DateTime);

            config.NewConfig<DateTimeOffset?, DateTime?>()
                .MapWith(src => src != null ? src.Value.DateTime : default(DateTime?));

            config.NewConfig<IFormFile, byte[]>()
                .MapWith(src => src.ToArray());
        }
    }
}