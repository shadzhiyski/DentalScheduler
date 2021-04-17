using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Identity.Dto.Output;
using DentalSystem.Entities.Identity;
using DentalSystem.Boundaries.Infrastructure.Identity;
using DentalSystem.Boundaries.UseCases.Identity.Dto.Output;
using DentalSystem.Boundaries.UseCases.Identity.Queries;
using Mapster;

namespace DentalSystem.Application.UseCases.Identity.Queries
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