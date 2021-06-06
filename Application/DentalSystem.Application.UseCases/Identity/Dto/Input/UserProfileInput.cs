using DentalSystem.Common.Helpers.Extensions;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;
using Microsoft.AspNetCore.Http;
using MediatR;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;

namespace DentalSystem.Application.UseCases.Identity.Dto.Input
{
    public record UserProfileInput : IUserProfileInput, IRequest<IResult<IMessageOutput>>
    {
        public IFormFile Avatar { get; init; }

        byte[] IUserProfileInput.Avatar => Avatar.ToArray();

        public string FirstName { get; init; }

        public string LastName { get; init; }
    }
}