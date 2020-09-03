using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using DentalScheduler.Web.UI.Models;
using Microsoft.Extensions.Options;

namespace DentalScheduler.Web.UI.Services
{
    public class DentalTeamService : IDentalTeamService
    {
        public DentalTeamService(
            HttpClient httpClient, 
            IOptions<AppSettings> appSettings,
            ILocalStorageService localStorage)
        {
            HttpClient = httpClient;
            AppSettings = appSettings.Value;
            LocalStorage = localStorage;

            HttpClient.BaseAddress = new Uri($"{AppSettings.ApiBaseAddress}odata/");
        }

        public HttpClient HttpClient { get; }
        
        public AppSettings AppSettings { get; }

        public ILocalStorageService LocalStorage { get; }

        public async Task<IList<DentalTeamDropDownViewModel>> GetDentalTeamsDropDownListAsync()
        {
            await SetAccessTokenAsync();

            var response = await HttpClient.GetAsync("DentalTeam");

            var result = (await response.Content.ReadFromJsonAsync<List<DentalTeamDropDownViewModel>>())
                .ToList();

            return result;
        }

        private async Task SetAccessTokenAsync()
        {
            var accessToken = await LocalStorage.GetItemAsync<string>("AccessToken");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}