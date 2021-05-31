using System;
using System.Linq;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Identity.Dto.Input;
using DentalSystem.Application.UseCases.Identity.Dto.Output;
using DentalSystem.Application.UseCases.Common.Dto.Output;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Entities.Identity;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using DentalSystem.Application.Boundaries.Infrastructure.Identity;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Output;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;
using DentalSystem.Application.Boundaries.UseCases.Identity.Commands;
using DentalSystem.Application.Boundaries.UseCases.Common.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace DentalSystem.Application.UseCases.Identity.Commands
{
    public class RegisterUserCommand : IRegisterUserCommand
    {
        public IConfiguration Config { get; }

        public IUserService<User> UserService { get; }

        public IApplicationValidator<IUserCredentialsInput> Validator { get; }

        public IJwtAuthManager JwtAuthManager { get; }

        public ILoginCommand LoginCommand { get; }

        public ILinkUserAndRoleCommand LinkUserAndRoleCommand { get; }

        public IWriteRepository<Patient> PatientRepo { get; }

        public IUnitOfWork UoW { get; }

        public RegisterUserCommand(
            IConfiguration config,
            IUserService<User> userService,
            IApplicationValidator<IUserCredentialsInput> validator,
            IJwtAuthManager jwtAuthManager,
            ILoginCommand loginCommand,
            ILinkUserAndRoleCommand linkUserAndRoleCommand,
            IWriteRepository<Patient> patientRepo,
            IUnitOfWork uoW)
        {
            Config = config;
            UserService = userService;
            Validator = validator;
            JwtAuthManager = jwtAuthManager;
            LoginCommand = loginCommand;
            LinkUserAndRoleCommand = linkUserAndRoleCommand;
            PatientRepo = patientRepo;
            UoW = uoW;
        }

        public async Task<IResult<IAccessTokenOutput>> RegisterAsync(IRegisterUserInput userInput, CancellationToken cancellationToken)
        {
            var validationResult = Validator.Validate(userInput);
            if (validationResult.Errors.Count > 0)
            {
                return new Result<IAccessTokenOutput>(validationResult.Errors);
            }

            var user = new User
            {
                Email = userInput.UserName,
                UserName = userInput.UserName,
                FirstName = userInput.FirstName,
                LastName = userInput.LastName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var identityResult = await UserService.CreateAsync(user, userInput.Password, cancellationToken);

            if (!identityResult.Succeeded)
            {
                return new Result<IAccessTokenOutput>(identityResult.Errors);
            }

            var linkUserWithRoleResult = await LinkUserAndRoleCommand.ExecuteAsync(new LinkUserAndRoleInput
            {
                UserName = userInput.UserName,
                RoleName = RoleType.Patient.ToString()
            }, cancellationToken);

            if (linkUserWithRoleResult.Errors.Count() > 0)
            {
                return new Result<IAccessTokenOutput>(linkUserWithRoleResult.Errors);
            }

            await PatientRepo.AddAsync(new Patient
            {
                IdentityUserId = user.Id,
            }, cancellationToken);

            await UoW.SaveAsync(cancellationToken);

            // Auto login after registrаtion (successful user registration should return access_token)
            return await LoginCommand.LoginAsync(new UserCredentialsInput()
            {
                UserName = userInput.UserName,
                Password = userInput.Password
            }, cancellationToken);
        }
    }
}