using System.Threading.Tasks;
using DentalSystem.UseCases.Identity.Dto.Output;
using DentalSystem.Entities.Identity;
using DentalSystem.Interfaces.Infrastructure.Identity;
using DentalSystem.Interfaces.UseCases.Identity.Dto.Output;
using DentalSystem.Interfaces.UseCases.Identity.Queries;
using Mapster;

namespace DentalSystem.UseCases.Identity.Queries
{
    public class GetUserProfileQuery : IGetUserProfileQuery
    {
        public GetUserProfileQuery(IUserService<User> userService)
        {
            UserService = userService;
        }

        public IUserService<User> UserService { get; }

        public async Task<IUserProfileOutput> ExecuteAsync()
        {
            var profile = await Task.FromResult(
                UserService.CurrentUser.Adapt<UserProfileOutput>()
            );

            profile.Avatar = profile.Avatar ?? new byte[0];

            return profile;
        }
    }
}