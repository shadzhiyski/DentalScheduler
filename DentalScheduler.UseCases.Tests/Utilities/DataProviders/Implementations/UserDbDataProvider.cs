using System;
using DentalScheduler.Entities;
using DentalScheduler.Entities.Identity;
using DentalScheduler.Interfaces.Infrastructure.Persistence;
using DentalScheduler.Interfaces.Infrastructure.Identity;
using System.Threading.Tasks;

namespace DentalScheduler.UseCases.Tests.Utilities.DataProviders
{
    public class UserDbDataProvider : IUserDbDataProvider
    {
        public UserDbDataProvider(
            IUserService<User> userService,
            IGenericRepository<Patient> patientRepository,
            IGenericRepository<DentalWorker> dentistRepository,
            IUnitOfWork uoW)
        {
            UserService = userService;
            PatientRepository = patientRepository;
            DentistRepository = dentistRepository;
            UoW = uoW;
        }

        public IUserService<User> UserService { get; }

        public IGenericRepository<Patient> PatientRepository { get; }

        public IGenericRepository<DentalWorker> DentistRepository { get; }

        public IUnitOfWork UoW { get; }

        public async Task<User> ProvideAdmin(string userName, string password)
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

        public async Task<(User, DentalWorker)> ProvideDentist(string userName, string password)
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

        public async Task<(User, Patient)> ProvidePatient(string userName, string password)
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
    }
}