using System.Threading.Tasks;
using DentalScheduler.DTO.Output.Common;
using DentalScheduler.Entities.Identity;
using DentalScheduler.Interfaces.Infrastructure.Identity;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output.Common;
using DentalScheduler.Interfaces.UseCases.Identity;
using DentalScheduler.Interfaces.UseCases.Common.Validation;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.UseCases.Identity
{
    public class CreateRoleCommand : ICreateRoleCommand
    {
        public CreateRoleCommand(
            IUserService<User> userService,
            IRoleService<IdentityRole> roleService,
            IApplicationValidator<ICreateRoleInput> validator)
        {
            UserService = userService;
            RoleService = roleService;
            Validator = validator;
        }

        public IUserService<User> UserService { get; }

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