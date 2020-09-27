using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using DentalScheduler.DTO.Output;
using DentalScheduler.Interfaces.Models.Output;
using DentalScheduler.Web.UI.Models;

namespace DentalScheduler.Web.UI.Services
{
    public class UserService : IUserService
    {
        public UserService(
            HttpClient httpClient,
            ILocalStorageService localStorage)
        {
            HttpClient = httpClient;
            LocalStorage = localStorage;
        }

        public HttpClient HttpClient { get; }

        public ILocalStorageService LocalStorage { get; }

        public async Task<IUserProfileOutput> GetProfile()
            => await (
                await HttpClient.GetAsync("api/User/profile")
            )
            .Content
            .ReadFromJsonAsync<UserProfileOutput>();
    }
}