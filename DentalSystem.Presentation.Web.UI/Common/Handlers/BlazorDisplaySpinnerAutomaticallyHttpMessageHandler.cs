using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DentalSystem.Presentation.Web.UI.Common.Services;

namespace DentalSystem.Presentation.Web.UI.Common.Handlers
{
    public class BlazorDisplaySpinnerAutomaticallyHttpMessageHandler : DelegatingHandler
    {
        public BlazorDisplaySpinnerAutomaticallyHttpMessageHandler(
            ISpinnerService spinnerService)
        {
            SpinnerService = spinnerService;
        }

        public ISpinnerService SpinnerService { get; }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            SpinnerService.Show();

            var response = await base.SendAsync(request, cancellationToken);

            SpinnerService.Hide();

            return response;
        }
    }
}