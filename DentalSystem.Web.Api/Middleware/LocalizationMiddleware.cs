using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DentalSystem.Web.Api.Middleware
{
    public static class LocalizationMiddleware
    {
        public static IServiceCollection AddLocalizationMiddleware(this IServiceCollection services)
            => services
                .AddLocalization()
                .Configure<RequestLocalizationOptions>(opts =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("bg-BG"),
                        new CultureInfo("en-US")
                    };

                    opts.DefaultRequestCulture = new RequestCulture("en-GB");
                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;
                });

        public static IApplicationBuilder UseLocalizationMiddleware(this IApplicationBuilder app)
            => app.UseRequestLocalization(
                    app.ApplicationServices
                        .GetService<IOptions<RequestLocalizationOptions>>()
                        .Value
                );
    }
}