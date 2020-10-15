using System;

namespace DentalScheduler.Web.UI.Services
{
    public interface ISpinnerService
    {
        event Action OnShow;

        event Action OnHide;

        void Show();

        void Hide();
    }
}