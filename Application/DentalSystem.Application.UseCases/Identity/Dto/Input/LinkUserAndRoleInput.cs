using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;

namespace DentalSystem.Application.UseCases.Identity.Dto.Input
{
    public record LinkUserAndRoleInput : ILinkUserAndRoleInput
    {
        public string UserName { get; init; }

        public string RoleName { get; init; }
    }
}