using DentalScheduler.Interfaces.Models.Output;

namespace DentalScheduler.Dto.Output
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