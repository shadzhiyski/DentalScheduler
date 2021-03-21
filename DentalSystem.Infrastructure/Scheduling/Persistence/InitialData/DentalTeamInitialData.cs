
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Interfaces.Infrastructure.Common.Persistence;

namespace DentalSystem.Infrastructure.Scheduling.Persistence.InitialData
{
    public class DentalTeamInitialData : IInitialData
    {
        public Type EntityType => typeof(DentalTeam);

        public int Priority => 5;

        public IEnumerable<object> GetData()
        => new List<DentalTeam>
        {
            new DentalTeam
            {
                Id = new Guid("451ecdbe-6ff4-49cb-a8b1-9862dba44f32"),
                Name = "DentalTeam 01",
                RoomId = new Guid("75f5f7fa-0ca1-41b9-aab9-fa631bd2bf55")
            },
            new DentalTeam
            {
                Id = new Guid("bfc227b5-eebc-4183-901d-29230fdda49a"),
                Name = "DentalTeam 02",
                RoomId = new Guid("1b25130c-d75c-4daa-a408-45f1dd890ec9")
            }
        };

        public Task<bool> InitData()
        {
            return Task.FromResult(true);
        }
    }
}