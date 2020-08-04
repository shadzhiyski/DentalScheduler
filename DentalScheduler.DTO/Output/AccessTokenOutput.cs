using DentalScheduler.Interfaces.Models.Output;

namespace DentalScheduler.DTO.Output
{
    public class AccessTokenOutput : IAccessTokenOutput
    {
        public AccessTokenOutput(string accessToken)
        {
            AccessToken = accessToken;
        }
        
        public string AccessToken { get; }
    }
}