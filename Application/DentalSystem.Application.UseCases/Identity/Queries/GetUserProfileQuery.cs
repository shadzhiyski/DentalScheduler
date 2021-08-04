using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Identity.Dto.Output;
using DentalSystem.Domain.Identity;
using DentalSystem.Application.Boundaries.Infrastructure.Identity;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Output;
using Mapster;
using MediatR;
using System.Threading;

namespace DentalSystem.Application.UseCases.Identity.Queries
{
    public record GetUserProfileInput() : IRequest<IUserProfileOutput>;

    public class GetUserProfileQuery : IRequestHandler<GetUserProfileInput, IUserProfileOutput>
    {
        public GetUserProfileQuery(IUserService<User> userService)
        {
            UserService = userService;
        }

        public IUserService<User> UserService { get; }

        public async Task<IUserProfileOutput> Handle(GetUserProfileInput request, CancellationToken cancellationToken)
        {
            var profile = await Task.FromResult(
                UserService.CurrentUser.Adapt<UserProfileOutput>()
            );

            profile = profile
                with { Avatar = profile.Avatar ?? new byte[0] };

            return profile;
        }
    }
}