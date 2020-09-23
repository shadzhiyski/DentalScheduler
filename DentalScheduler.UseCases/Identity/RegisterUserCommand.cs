using System;
using System.Linq;
using System.Threading.Tasks;
using DentalScheduler.DTO.Input;
using DentalScheduler.DTO.Output;
using DentalScheduler.DTO.Output.Common;
using DentalScheduler.Entities;
using DentalScheduler.Entities.Identity;
using DentalScheduler.Interfaces.Infrastructure.Persistence;
using DentalScheduler.Interfaces.Infrastructure.Identity;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output;
using DentalScheduler.Interfaces.Models.Output.Common;
using DentalScheduler.Interfaces.UseCases.Identity;
using DentalScheduler.Interfaces.UseCases.Common.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace DentalScheduler.UseCases.Identity
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
                RoleName = userInput.RoleType.ToString()
            });     

            if (linkUserWithRoleResult.Errors.Count() > 0)
            {
                return new Result<IAccessTokenOutput>(linkUserWithRoleResult.Errors);
            }

            if (userInput.RoleType.Equals(RoleType.Patient))
            {
                PatientRepo.Add(new Patient
                {
                    IdentityUserId = user.Id,

                });
            }
            else
            {
                var dentalWorker = new DentalWorker
                {
                    IdentityUserId = user.Id,
                    JobType = JobType.Dentist
                };

                DentalWorkerRepo.Add(dentalWorker);
            }

            UoW.Save();

            // Auto login after registrаtion (successful user registration should return access_token)
            return await LoginCommand.LoginAsync(new UserCredentialsInput()
            {
                UserName = userInput.UserName,
                Password = userInput.Password
            });
        }
    }
}