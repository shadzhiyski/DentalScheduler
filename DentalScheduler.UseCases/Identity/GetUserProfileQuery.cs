using System.Threading.Tasks;
using DentalScheduler.DTO.Output;
using DentalScheduler.Entities.Identity;
using DentalScheduler.Interfaces.Infrastructure.Identity;
using DentalScheduler.Interfaces.Models.Output;
using DentalScheduler.Interfaces.UseCases.Identity;
using Mapster;

namespace DentalScheduler.UseCases.Identity
{
    public class GetUserProfileQuery : IGetUserProfileQuery
    {
        public GetUserProfileQuery(IUserService<User> userService)
        {
            UserService = userService;
        }

        public IUserService<User> UserService { get; }

        public async Task<IUserProfileOutput> ExecuteAsync()
            => await Task.FromResult(
                UserService.CurrentUser.Adapt<UserProfileOutput>()
            );
    }
}