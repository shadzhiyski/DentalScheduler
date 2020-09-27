using System.Threading.Tasks;
using DentalScheduler.Interfaces.Models.Output;

namespace DentalScheduler.Web.UI.Services
{
    public interface IUserService
    {
        Task<IUserProfileOutput> GetProfile();
    }
}