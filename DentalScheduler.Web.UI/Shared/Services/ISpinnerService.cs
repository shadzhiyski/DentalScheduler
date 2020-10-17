using System;

namespace DentalScheduler.Web.UI.Shared.Services
{
    public interface ISpinnerService
    {
        event Action OnShow;

        event Action OnHide;

        void Show();

        void Hide();
    }
}