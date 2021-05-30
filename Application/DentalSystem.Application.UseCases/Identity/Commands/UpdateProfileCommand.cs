using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Common.Dto.Output;
using DentalSystem.Entities.Identity;
using DentalSystem.Application.Boundaries.Infrastructure.Identity;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;
using DentalSystem.Application.Boundaries.UseCases.Common.Validation;
using DentalSystem.Application.Boundaries.UseCases.Identity.Commands;
using Mapster;
using System.Threading;

namespace DentalSystem.Application.UseCases.Identity.Commands
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

        public async Task<IResult<IMessageOutput>> ExecuteAsync(IUserProfileInput input, CancellationToken cancellationToken)
        {
            var validationResult = Validator.Validate(input);
            if (validationResult.Errors.Count > 0)
            {
                return new Result<IMessageOutput>(validationResult.Errors);
            }

            var user = UserService.CurrentUser;

            input.Adapt(user, MappingConfig);

            await UserService.UpdateAsync(user, cancellationToken);

            return new Result<IMessageOutput>(new MessageOutput("User profile successfully updated."));
        }
    }
}