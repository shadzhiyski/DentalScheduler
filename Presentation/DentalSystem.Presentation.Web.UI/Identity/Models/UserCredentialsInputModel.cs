using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;

namespace DentalSystem.Presentation.Web.UI.Identity.Models
{
    public class UserCredentialsInputModel : IUserCredentialsInput
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}