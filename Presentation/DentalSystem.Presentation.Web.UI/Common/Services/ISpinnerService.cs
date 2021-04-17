using System;

namespace DentalSystem.Presentation.Web.UI.Common.Services
{
    public interface ISpinnerService
    {
        event Action OnShow;

        event Action OnHide;

        void Show();

        void Hide();
    }
}