using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace DentalScheduler.Web.UI.Handlers 
{
    public class AuthorizationHeaderHttpHandler : DelegatingHandler 
    {
        public AuthorizationHeaderHttpHandler(ILocalStorageService localStorage) 
        {
            LocalStorage = localStorage;
        }

        public ILocalStorageService LocalStorage { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken) 
        {
            if (request.Headers.Authorization == default)
            {
                var accessToken = await LocalStorage.GetItemAsync<string>("AccessToken");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}