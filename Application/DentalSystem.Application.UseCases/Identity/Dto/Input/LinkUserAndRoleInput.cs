using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;
using MediatR;

namespace DentalSystem.Application.UseCases.Identity.Dto.Input
{
    public record LinkUserAndRoleInput : ILinkUserAndRoleInput, IRequest<IResult<IMessageOutput>>
    {
        public string UserName { get; init; }

        public string RoleName { get; init; }
    }
}