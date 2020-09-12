using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using DentalScheduler.Web.UI.Models;
using Microsoft.Extensions.Options;

namespace DentalScheduler.Web.UI.Services
{
    public class DentalTeamService : BaseDataService, IDentalTeamService
    {
        public DentalTeamService(
                HttpClient httpClient, 
                IOptions<AppSettings> appSettings,
                ILocalStorageService localStorage)
            : base(httpClient, localStorage)
        {
            AppSettings = appSettings.Value;

            HttpClient.BaseAddress = new Uri($"{AppSettings.ApiBaseAddress}odata/");
        }
        
        public AppSettings AppSettings { get; }

        public async Task<IList<DentalTeamDropDownViewModel>> GetDentalTeamsDropDownListAsync()
        {
            await SetAccessTokenAsync();

            var result = await ODataClient
                .For<DentalTeamDropDownViewModel>("DentalTeam")
                .FindEntriesAsync();

            return result.ToList();
        }
    }
}