namespace DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input
{
    public interface ILinkUserAndRoleInput
    {
        string UserName { get; }

        string RoleName { get; }
    }
}