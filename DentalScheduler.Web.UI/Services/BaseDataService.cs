using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.Extensions.Options;

namespace DentalScheduler.Web.UI.Services
{
    public abstract class BaseDataService
    {
        public BaseDataService(
            HttpClient httpClient,
            ILocalStorageService localStorage)
        {
            HttpClient = httpClient;
            LocalStorage = localStorage;
        }

        public HttpClient HttpClient { get; }

        public ILocalStorageService LocalStorage { get; }

        protected async Task SetAccessTokenAsync()
        {
            var accessToken = await LocalStorage.GetItemAsync<string>("AccessToken");

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}