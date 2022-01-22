namespace DentalSystem.Presentation.Web.Api
{
    public record AppSettings
    {
        public string[] AllowedOrigins { get; init; }
    }
}