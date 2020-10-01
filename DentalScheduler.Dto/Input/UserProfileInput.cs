using DentalScheduler.Common.Helpers.Extensions;
using DentalScheduler.Interfaces.Dto.Input;
using Microsoft.AspNetCore.Http;

namespace DentalScheduler.Dto.Input
{
    public class UserProfileInput : IUserProfileInput
    {
        public IFormFile Avatar { get; set; }

        byte[] IUserProfileInput.Avatar => Avatar.ToArray();

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}