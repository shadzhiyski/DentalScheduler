using DentalSystem.Common.Helpers.Extensions;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;
using Microsoft.AspNetCore.Http;

namespace DentalSystem.Application.UseCases.Identity.Dto.Input
{
    public class UserProfileInput : IUserProfileInput
    {
        public IFormFile Avatar { get; set; }

        byte[] IUserProfileInput.Avatar => Avatar.ToArray();

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}