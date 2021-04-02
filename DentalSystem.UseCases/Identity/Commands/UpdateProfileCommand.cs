using System.Threading.Tasks;
using DentalSystem.UseCases.Common.Dto.Output;
using DentalSystem.Entities.Identity;
using DentalSystem.Interfaces.Infrastructure.Identity;
using DentalSystem.Interfaces.UseCases.Identity.Dto.Input;
using DentalSystem.Interfaces.UseCases.Common.Dto.Output;
using DentalSystem.Interfaces.UseCases.Common.Validation;
using DentalSystem.Interfaces.UseCases.Identity.Commands;
using Mapster;

namespace DentalSystem.UseCases.Identity.Commands
{
    public class UpdateProfileCommand : IUpdateProfileCommand
    {
        public UpdateProfileCommand(
            TypeAdapterConfig mappingConfig,
            IApplicationValidator<IUserProfileInput> validator,
            IUserService<User> userService)
        {
            MappingConfig = mappingConfig;
            Validator = validator;
            UserService = userService;
        }

        public TypeAdapterConfig MappingConfig { get; }

        public IApplicationValidator<IUserProfileInput> Validator { get; }

        public IUserService<User> UserService { get; }

        public async Task<IResult<IMessageOutput>> ExecuteAsync(IUserProfileInput input)
        {
            var validationResult = Validator.Validate(input);
            if (validationResult.Errors.Count > 0)
            {
                return new Result<IMessageOutput>(validationResult.Errors);
            }

            var user = UserService.CurrentUser;

            input.Adapt(user, MappingConfig);

            await UserService.UpdateAsync(user);

            return new Result<IMessageOutput>(new MessageOutput("User profile successfully updated."));
        }
    }
}