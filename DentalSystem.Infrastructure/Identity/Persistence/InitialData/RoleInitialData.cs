using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalSystem.Entities.Identity;
using DentalSystem.Boundaries.Infrastructure.Common.Persistence;
using DentalSystem.Boundaries.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace DentalSystem.Infrastructure.Identity.Persistence.InitialData
{
    public class RoleInitialData : IInitialData
    {
        public RoleInitialData(IRoleService<IdentityRole> roleService)
        {
            RoleService = roleService;
        }

        public Type EntityType => typeof(User);

        public IRoleService<IdentityRole> RoleService { get; }

        public int Priority => 0;

        public IEnumerable<object> GetData()
            => new List<IdentityRole>
            {
                new IdentityRole { Name = "Dentist" },
                new IdentityRole { Name = "DentistAssistant" },
                new IdentityRole { Name = "Patient" }
            };

        public async Task<bool> InitData()
        {
            foreach (var role in GetData().Cast<IdentityRole>())
            {
                await RoleService.CreateAsync(role);
            }

            return false;
        }
    }
}