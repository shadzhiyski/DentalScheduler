using Microsoft.AspNetCore.Builder;

namespace DentalSystem.Presentation.Web.Api.Middleware
{
    public static class CorsMiddleware
    {
        public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder app)
            => app.UseCors(policy => policy
                    .WithOrigins(new string[] { "https://localhost:5001" })
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                );
    }
}