using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;
using DentalSystem.Common.Helpers.Extensions;
using Microsoft.AspNetCore.Http;

namespace DentalSystem.Presentation.Web.UI.Identity.Models
{
    public class UserProfileInputModel : IUserProfileInput
    {
        public IFormFile Avatar { get; set; }

        byte[] IUserProfileInput.Avatar => Avatar.ToArray();

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}