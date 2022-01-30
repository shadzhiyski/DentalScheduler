using Microsoft.AspNetCore.Builder;

namespace DentalSystem.Presentation.Web.Api.Common.Middleware
{
    public static class CorsMiddleware
    {
        public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder app, AppSettings appSettings)
            => app.UseCors(policy => policy
                    .WithOrigins(appSettings.AllowedOrigins ?? new string[0])
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                );
    }
}