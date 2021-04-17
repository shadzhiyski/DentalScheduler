using System.Threading.Tasks;
using DentalSystem.Boundaries.UseCases.Identity.Dto.Output;

namespace DentalSystem.Boundaries.UseCases.Identity.Queries
{
    public interface IGetUserProfileQuery
    {
        Task<IUserProfileOutput> ExecuteAsync();
    }
}