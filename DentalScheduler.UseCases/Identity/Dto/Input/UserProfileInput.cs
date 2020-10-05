using DentalScheduler.Common.Helpers.Extensions;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;
using Microsoft.AspNetCore.Http;

namespace DentalScheduler.UseCases.Identity.Dto.Input
{
    public class UserProfileInput : IUserProfileInput
    {
        public IFormFile Avatar { get; set; }

        byte[] IUserProfileInput.Avatar => Avatar.ToArray();

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}