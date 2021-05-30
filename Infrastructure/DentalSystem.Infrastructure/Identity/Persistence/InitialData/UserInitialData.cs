using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalSystem.Entities.Identity;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using DentalSystem.Application.Boundaries.Infrastructure.Identity;
using System.Threading;

namespace DentalSystem.Infrastructure.Identity.Persistence.InitialData
{
    public class UserInitialData : IInitialData
    {
        public UserInitialData(IUserService<User> userService)
        {
            UserService = userService;
        }

        public Type EntityType => typeof(User);

        public IUserService<User> UserService { get; }

        public int Priority => 1;

        public IEnumerable<object> GetData()
            => new List<User>
            {
                new User
                {
                    Id = "0a617280-ed83-4447-93ba-f8ed7aaae62b",
                    Email = "dentist_01@mail.com",
                    UserName = "dentist_01@mail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = "Dentist 01",
                    LastName = "Dentist 01"
                },
                new User
                {
                    Id = "3c9e331e-b7c7-4667-8615-b71b9e4187f1",
                    Email = "dentist_02@mail.com",
                    UserName = "dentist_02@mail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = "Dentist 02",
                    LastName = "Dentist 02"
                },
                new User
                {
                    Id = "74da05e6-876e-4457-823d-440e5c1686cd",
                    Email = "dentistAssistant_01@mail.com",
                    UserName = "dentistAssistant_01@mail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = "Assistant 01",
                    LastName = "Assistant 01"
                },
                new User
                {
                    Id = "d72bb325-d935-4da3-bb7d-fdd748c9878a",
                    Email = "dentistAssistant_02@mail.com",
                    UserName = "dentistAssistant_02@mail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = "Assistant 02",
                    LastName = "Assistant 02"
                }
            };

        public async Task<bool> InitData(CancellationToken cancellationToken)
        {
            var users = GetData().Cast<User>().ToList();
            await UserService.CreateAsync(users[0], "Dentist_01#123456", cancellationToken);
            await UserService.AddToRoleAsync(users[0], "Dentist", cancellationToken);
            await UserService.CreateAsync(users[1], "Dentist_02#123456", cancellationToken);
            await UserService.AddToRoleAsync(users[1], "Dentist", cancellationToken);

            await UserService.CreateAsync(users[2], "DentistAssistant_01#123456", cancellationToken);
            await UserService.AddToRoleAsync(users[2], "DentistAssistant", cancellationToken);
            await UserService.CreateAsync(users[3], "DentistAssistant_02#123456", cancellationToken);
            await UserService.AddToRoleAsync(users[3], "DentistAssistant", cancellationToken);

            return false;
        }
    }
}