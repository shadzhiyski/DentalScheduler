using DentalScheduler.Interfaces.UseCases.Identity.Dto.Output;

namespace DentalScheduler.UseCases.Identity.Dto.Output
{
    public class AccessTokenOutput : IAccessTokenOutput
    {
        public AccessTokenOutput()
        { }

        public AccessTokenOutput(string accessToken)
        {
            AccessToken = accessToken;
        }

        public string AccessToken { get; set; }
    }
}