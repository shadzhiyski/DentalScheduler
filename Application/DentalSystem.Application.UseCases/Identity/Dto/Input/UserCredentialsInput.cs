using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Output;
using MediatR;

namespace DentalSystem.Application.UseCases.Identity.Dto.Input
{
    public record UserCredentialsInput : IUserCredentialsInput, IRequest<IResult<IAccessTokenOutput>>
    {
        public string UserName { get; init; }

        public string Password { get; init; }
    }
}