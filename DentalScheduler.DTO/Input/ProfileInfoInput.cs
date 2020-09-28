using DentalScheduler.Common.Helpers.Extensions;
using DentalScheduler.Interfaces.Models.Input;
using Microsoft.AspNetCore.Http;

namespace DentalScheduler.DTO.Input
{
    public class ProfileInfoInput : IProfileInfoInput
    {
        public IFormFile Avatar { get; set; }

        byte[] IProfileInfoInput.Avatar => Avatar.ToArray();

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}