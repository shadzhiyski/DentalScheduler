using System;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Gateways;
using DentalScheduler.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.UseCases.Tests.Utilities.DataProviders
{
    public class UserDbDataProvider : IUserDbDataProvider
    {
        public UserDbDataProvider(
            IUserService<IdentityUser> userService,
            IGenericRepository<Patient> patientRepository,
            IGenericRepository<DentalWorker> dentistRepository,
            IUnitOfWork uoW)
        {
            UserService = userService;
            PatientRepository = patientRepository;
            DentistRepository = dentistRepository;
            UoW = uoW;
        }

        public IUserService<IdentityUser> UserService { get; }

        public IGenericRepository<Patient> PatientRepository { get; }

        public IGenericRepository<DentalWorker> DentistRepository { get; }

        public IUnitOfWork UoW { get; }

        public IdentityUser ProvideAdmin(string userName, string password)
        {
            var user = new IdentityUser
            {
                Email = userName,
                UserName = userName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            UserService.CreateAsync(user, password);
            UserService.AddToRoleAsync(user, "Admin");

            return user;
        }

        public (IdentityUser, DentalWorker) ProvideDentist(string userName, string password)
        {
            var user = new IdentityUser
            {
                Email = userName,
                UserName = userName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            UserService.CreateAsync(user, password);
            UserService.AddToRoleAsync(user, "Dentist");

            var dentist = new DentalWorker
            {
                IdentityUserId = user.Id,
                JobType = JobType.Dentist
            };

            DentistRepository.Add(dentist);

            UoW.Save();

            return (user, dentist);
        }

        public (IdentityUser, Patient) ProvidePatient(string userName, string password)
        {
            var user = new IdentityUser
            {
                Email = userName,
                UserName = userName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            UserService.CreateAsync(user, password);
            UserService.AddToRoleAsync(user, "Patient");

            var patient = new Patient
            {
                IdentityUserId = user.Id
            };

            PatientRepository.Add(patient);

            UoW.Save();

            return (user, patient);
        }
    }
}