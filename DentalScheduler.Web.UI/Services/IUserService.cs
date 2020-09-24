using System.Threading.Tasks;
using DentalScheduler.Web.UI.Models;

namespace DentalScheduler.Web.UI.Services
{
    public interface IUserService
    {
        Task<UserProfileViewModel> GetProfile();
    }
}