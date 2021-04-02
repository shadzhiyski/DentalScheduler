using System.Threading.Tasks;
using DentalSystem.UseCases.Common.Dto.Output;
using DentalSystem.Entities.Identity;
using DentalSystem.Interfaces.Infrastructure.Identity;
using DentalSystem.Interfaces.UseCases.Identity.Dto.Input;
using DentalSystem.Interfaces.UseCases.Common.Dto.Output;
using DentalSystem.Interfaces.UseCases.Identity.Commands;
using DentalSystem.Interfaces.UseCases.Common.Validation;
using Microsoft.AspNetCore.Identity;

namespace DentalSystem.UseCases.Identity.Commands
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