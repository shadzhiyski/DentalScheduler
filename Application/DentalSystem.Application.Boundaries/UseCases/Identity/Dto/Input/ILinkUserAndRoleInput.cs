namespace DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input
{
    public interface ILinkUserAndRoleInput
    {
        string UserName { get; set; }

        string RoleName { get; set; }
    }
}