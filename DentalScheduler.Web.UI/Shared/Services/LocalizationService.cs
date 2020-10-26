using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace DentalScheduler.Web.UI.Shared.Services
{
    public class LocalizationService : ILocalizationService
    {
        private const string BlazorCulturePropertyName = "blazorCulture";

        public LocalizationService(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        public IJSRuntime JsRuntime { get; }

        public IReadOnlyCollection<CultureInfo> GetAvaliableCultures()
            => new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("bg-BG"),
            }.AsReadOnly();

        public async Task SetDefaultCultureAsync()
        {
            var result = await JsRuntime.InvokeAsync<string>($"{BlazorCulturePropertyName}.get");
            if (result != null)
            {
                var culture = new CultureInfo(result);
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }
        }

        public bool SetCulture(CultureInfo culture)
        {
            var isChanged = false;
            if (CultureInfo.CurrentCulture != culture)
            {
                var jsProcessRuntime = (IJSInProcessRuntime)JsRuntime;
                jsProcessRuntime.InvokeVoid($"{BlazorCulturePropertyName}.set", culture.Name);

                isChanged = true;
            }

            return isChanged;
        }
    }
}