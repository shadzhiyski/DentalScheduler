using System;
using DentalSystem.Entities.Identity;
using DentalSystem.Interfaces.Infrastructure.Common.Persistence;
using DentalSystem.Interfaces.Infrastructure.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using DentalSystem.Entities.Scheduling;

namespace DentalSystem.UseCases.Tests.Utilities.DataProviders
{
    public class UserDbDataProvider : IUserDbDataProvider
    {
        public UserDbDataProvider(
            IUserService<User> userService,
            IRoleService<IdentityRole> roleService,
            IGenericRepository<Patient> patientRepository,
            IGenericRepository<DentalWorker> dentistRepository,
            IUnitOfWork uoW)
        {
            UserService = userService;
            RoleService = roleService;
            PatientRepository = patientRepository;
            DentistRepository = dentistRepository;
            UoW = uoW;
        }

        public IUserService<User> UserService { get; }

        public IRoleService<IdentityRole> RoleService { get; }

        public IGenericRepository<Patient> PatientRepository { get; }

        public IGenericRepository<DentalWorker> DentistRepository { get; }

        public IUnitOfWork UoW { get; }

        public async Task<User> ProvideAdminAsync(string userName, string password)
        {
            var user = new User
            {
                Email = userName,
                UserName = userName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            await UserService.CreateAsync(user, password);
            await UserService.AddToRoleAsync(user, "Admin");

            return user;
        }

        public async Task<(User, DentalWorker)> ProvideDentistAsync(string userName, string password)
        {
            var user = new User
            {
                Email = userName,
                UserName = userName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            await UserService.CreateAsync(user, password);
            await UserService.AddToRoleAsync(user, "Dentist");

            var dentist = new DentalWorker
            {
                IdentityUserId = user.Id,
                JobType = JobType.Dentist
            };

            await DentistRepository.AddAsync(dentist);

            await UoW.SaveAsync();

            return (user, dentist);
        }

        public async Task<(User, Patient)> ProvidePatientAsync(string userName, string password)
        {
            var user = new User
            {
                Email = userName,
                UserName = userName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            await UserService.CreateAsync(user, password);
            await UserService.AddToRoleAsync(user, "Patient");

            var patient = new Patient
            {
                IdentityUserId = user.Id
            };

            await PatientRepository.AddAsync(patient);

            await UoW.SaveAsync();

            return (user, patient);
        }

        public async Task<IEnumerable<IdentityRole>> ProvideRolesAsync(params string[] roles)
        {
            var identityRoles = new List<IdentityRole>();
            foreach (var role in roles)
            {
                var identityRole = new IdentityRole()
                {
                    Name = role
                };

                await RoleService.CreateAsync(identityRole);
                identityRoles.Add(identityRole);
            }

            return identityRoles;
        }
    }
}