using System;
using System.Linq;
using System.Threading.Tasks;
using DentalSystem.UseCases.Identity.Dto.Input;
using DentalSystem.UseCases.Identity.Dto.Output;
using DentalSystem.UseCases.Common.Dto.Output;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Entities.Identity;
using DentalSystem.Interfaces.Infrastructure.Common.Persistence;
using DentalSystem.Interfaces.Infrastructure.Identity;
using DentalSystem.Interfaces.UseCases.Identity.Dto.Input;
using DentalSystem.Interfaces.UseCases.Identity.Dto.Output;
using DentalSystem.Interfaces.UseCases.Common.Dto.Output;
using DentalSystem.Interfaces.UseCases.Identity.Commands;
using DentalSystem.Interfaces.UseCases.Common.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace DentalSystem.UseCases.Identity.Commands
{
    public class RegisterUserCommand : IRegisterUserCommand
    {
        public IConfiguration Config { get; }

        public IUserService<User> UserService { get; }

        public IApplicationValidator<IUserCredentialsInput> Validator { get; }

        public IJwtAuthManager JwtAuthManager { get; }

        public ILoginCommand LoginCommand { get; }

        public ILinkUserAndRoleCommand LinkUserAndRoleCommand { get; }

        public IGenericRepository<Patient> PatientRepo { get; }

        public IGenericRepository<DentalWorker> DentalWorkerRepo { get; }

        public IUnitOfWork UoW { get; }

        public RegisterUserCommand(
            IConfiguration config,
            IUserService<User> userService,
            IApplicationValidator<IUserCredentialsInput> validator,
            IJwtAuthManager jwtAuthManager,
            ILoginCommand loginCommand,
            ILinkUserAndRoleCommand linkUserAndRoleCommand,
            IGenericRepository<Patient> patientRepo,
            IGenericRepository<DentalWorker> dentalWorkerRepo,
            IUnitOfWork uoW)
        {
            Config = config;
            UserService = userService;
            Validator = validator;
            JwtAuthManager = jwtAuthManager;
            LoginCommand = loginCommand;
            LinkUserAndRoleCommand = linkUserAndRoleCommand;
            PatientRepo = patientRepo;
            DentalWorkerRepo = dentalWorkerRepo;
            UoW = uoW;
        }

        public async Task<IResult<IAccessTokenOutput>> RegisterAsync(IRegisterUserInput userInput)
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

            var identityResult = await UserService.CreateAsync(user, userInput.Password);

            if (!identityResult.Succeeded)
            {
                return new Result<IAccessTokenOutput>(identityResult.Errors);
            }

            var linkUserWithRoleResult = await LinkUserAndRoleCommand.ExecuteAsync(new LinkUserAndRoleInput
            {
                UserName = userInput.UserName,
                RoleName = RoleType.Patient.ToString()
            });

            if (linkUserWithRoleResult.Errors.Count() > 0)
            {
                return new Result<IAccessTokenOutput>(linkUserWithRoleResult.Errors);
            }

            await PatientRepo.AddAsync(new Patient
            {
                IdentityUserId = user.Id,
            });

            await UoW.SaveAsync();

            // Auto login after registrаtion (successful user registration should return access_token)
            return await LoginCommand.LoginAsync(new UserCredentialsInput()
            {
                UserName = userInput.UserName,
                Password = userInput.Password
            });
        }
    }
}