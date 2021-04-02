using System.Threading.Tasks;
using DentalSystem.Interfaces.UseCases.Identity.Dto.Output;

namespace DentalSystem.Interfaces.UseCases.Identity.Queries
{
    public interface IGetUserProfileQuery
    {
        Task<IUserProfileOutput> ExecuteAsync();
    }
}