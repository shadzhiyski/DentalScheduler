using System.Threading.Tasks;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Output;

namespace DentalSystem.Application.Boundaries.UseCases.Identity.Queries
{
    public interface IGetUserProfileQuery
    {
        Task<IUserProfileOutput> ExecuteAsync();
    }
}