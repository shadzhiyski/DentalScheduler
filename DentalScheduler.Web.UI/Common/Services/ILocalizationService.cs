using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace DentalScheduler.Web.UI.Common.Services
{
    public interface ILocalizationService
    {
        Task<IReadOnlyCollection<CultureInfo>> GetAvaliableCulturesAsync();

        Task SetDefaultCultureAsync();

        Task<bool> SetCultureAsync(CultureInfo culture);
    }
}