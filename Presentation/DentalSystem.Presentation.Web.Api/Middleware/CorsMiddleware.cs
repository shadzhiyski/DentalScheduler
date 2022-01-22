using Microsoft.AspNetCore.Builder;

namespace DentalSystem.Presentation.Web.Api.Middleware
{
    public static class CorsMiddleware
    {
        public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder app, AppSettings appSettings)
            => app.UseCors(policy => policy
                    .WithOrigins(appSettings.AllowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                );
    }
}