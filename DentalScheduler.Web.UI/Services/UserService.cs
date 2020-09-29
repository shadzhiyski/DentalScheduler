using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using DentalScheduler.Dto.Output;
using DentalScheduler.Interfaces.Dto.Input;
using DentalScheduler.Interfaces.Dto.Output;

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

        public async Task SetProfile(IUserProfileInput input)
        {
            var content = new MultipartFormDataContent();

            content.Add(
                content: new ByteArrayContent(input.Avatar, 0, input.Avatar.Length), 
                name: nameof(input.Avatar), 
                fileName: nameof(input.Avatar)
            );

            content.Add(content: new StringContent(input.FirstName), name: nameof(input.FirstName));
            content.Add(content: new StringContent(input.LastName), name: nameof(input.LastName));

            await HttpClient.PostAsync("api/User/profile", content);
        }
    }
}