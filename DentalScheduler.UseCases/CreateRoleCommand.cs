using System.Threading.Tasks;
using DentalScheduler.DTO.Output.Common;
using DentalScheduler.Interfaces.Infrastructure;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output.Common;
using DentalScheduler.Interfaces.UseCases;
using DentalScheduler.Interfaces.UseCases.Validation;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.UseCases
{
    public class CreateRoleCommand : ICreateRoleCommand
    {
        public CreateRoleCommand(
            IUserService<IdentityUser> userService,
            IRoleService<IdentityRole> roleService,
            IApplicationValidator<ICreateRoleInput> validator)
        {
            UserService = userService;
            RoleService = roleService;
            Validator = validator;
        }

        public IUserService<IdentityUser> UserService { get; }

        public IRoleService<IdentityRole> RoleService { get; }
        
        public IApplicationValidator<ICreateRoleInput> Validator { get; }

        public async Task<IResult<IMessageOutput>> CreateAsync(ICreateRoleInput roleInput)
        {
            var validationResult = Validator.Validate(roleInput);
            if (validationResult.Errors.Count > 0)
            {
                return new Result<IMessageOutput>(validationResult.Errors);
            }

            var role = new IdentityRole()
            {
                Name = roleInput.Name
            };

            await RoleService.CreateAsync(role);

            return new Result<IMessageOutput>(
                new MessageOutput($"Role \"{roleInput.Name}\" created.")
            );
        }
    }
}