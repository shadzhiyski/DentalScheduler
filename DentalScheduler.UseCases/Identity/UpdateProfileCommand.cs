using System.Threading.Tasks;
using DentalScheduler.DTO.Output.Common;
using DentalScheduler.Entities.Identity;
using DentalScheduler.Interfaces.Infrastructure.Identity;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output.Common;
using DentalScheduler.Interfaces.UseCases.Common.Validation;
using DentalScheduler.Interfaces.UseCases.Identity;
using Mapster;

namespace DentalScheduler.UseCases.Identity
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