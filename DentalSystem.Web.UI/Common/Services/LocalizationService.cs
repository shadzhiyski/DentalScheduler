using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace DentalSystem.Web.UI.Common.Services
{
    public class LocalizationService : ILocalizationService
    {
        public LocalizationService(ILocalStorageService localStorage)
        {
            LocalStorage = localStorage;
        }

        public ILocalStorageService LocalStorage { get; }

        public Task<IReadOnlyCollection<CultureInfo>> GetAvaliableCulturesAsync()
            => Task.FromResult(new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("bg-BG"),
                }.AsReadOnly() as IReadOnlyCollection<CultureInfo>
            );

        public async Task SetDefaultCultureAsync()
        {
            if (await LocalStorage.ContainKeyAsync(LocalStorageKeys.Localization.CurrentCulture))
            {
                var result = await LocalStorage
                    .GetItemAsync<string>(LocalStorageKeys.Localization.CurrentCulture);

                var culture = new CultureInfo(result);
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }
        }

        public async Task<bool> SetCultureAsync(CultureInfo culture)
        {
            var isChanged = false;
            if (CultureInfo.CurrentCulture != culture)
            {
                await LocalStorage.SetItemAsync(
                    key: LocalStorageKeys.Localization.CurrentCulture,
                    data: culture.Name
                );

                isChanged = true;
            }

            return isChanged;
        }
    }
}