using System;
using Mapster;

namespace DentalScheduler.Config.Mappings
{
    public class CommonMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<DateTimeOffset, DateTime>()
                .MapWith(src => src.DateTime);
            
            config.NewConfig<DateTimeOffset?, DateTime?>()
                .MapWith(src => src != null ? src.Value.DateTime : default(DateTime?));
        }
    }
}