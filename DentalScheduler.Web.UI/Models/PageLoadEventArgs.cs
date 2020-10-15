namespace DentalScheduler.Web.UI.Models
{
    public class PageLoadEventArgs
    {
        public PageLoadEventArgs(int selectedPage)
        {
            SelectedPage = selectedPage;
        }

        public int SelectedPage { get; set; }
    }
}