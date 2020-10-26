using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace DentalScheduler.Web.UI.Shared.Services
{
    public interface ILocalizationService
    {
        IReadOnlyCollection<CultureInfo> GetAvaliableCultures();

        Task SetDefaultCultureAsync();

        bool SetCulture(CultureInfo culture);
    }
}