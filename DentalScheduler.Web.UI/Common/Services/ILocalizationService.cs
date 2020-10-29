using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace DentalScheduler.Web.UI.Common.Services
{
    public interface ILocalizationService
    {
        IReadOnlyCollection<CultureInfo> GetAvaliableCultures();

        Task SetDefaultCultureAsync();

        Task<bool> SetCultureAsync(CultureInfo culture);
    }
}