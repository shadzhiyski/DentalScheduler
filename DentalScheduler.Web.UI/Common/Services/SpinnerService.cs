using System;

namespace DentalScheduler.Web.UI.Common.Services
{
    public class SpinnerService : ISpinnerService
    {
        public event Action OnShow;
		public event Action OnHide;

		public void Show()
		{
			OnShow?.Invoke();
		}

		public void Hide()
		{
			OnHide?.Invoke();
		}
    }
}