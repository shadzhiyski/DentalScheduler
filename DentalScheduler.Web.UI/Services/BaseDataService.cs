using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Simple.OData.Client;

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

        public ODataClient ODataClient { get; private set; }

        protected async Task SetAccessTokenAsync()
        {
            var accessToken = await LocalStorage.GetItemAsync<string>("AccessToken");
            if (HttpClient.DefaultRequestHeaders.Authorization?.Parameter.Equals(accessToken) ?? false)
            {
                return;
            }

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var clientSettings = new ODataClientSettings(HttpClient);
            ODataClient = new ODataClient(clientSettings);

            System.Console.WriteLine("SetAccessTokenAsync executed.");
        }
    }
}