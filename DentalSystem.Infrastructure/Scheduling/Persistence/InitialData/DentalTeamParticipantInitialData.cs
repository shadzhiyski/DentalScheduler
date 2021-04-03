using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Interfaces.Infrastructure.Common.Persistence;

namespace DentalSystem.Infrastructure.Scheduling.Persistence.InitialData
{
    public class DentalTeamParticipantInitialData : IInitialData
    {
        public Type EntityType => typeof(DentalTeamParticipant);

        public int Priority => 6;

        public IEnumerable<object> GetData()
            => new List<DentalTeamParticipant>
            {
                new DentalTeamParticipant
                {
                    TeamId = new Guid("451ecdbe-6ff4-49cb-a8b1-9862dba44f32"),
                    ParticipantId = new Guid("3fb71697-6ae3-4300-b172-e64d580e3ef0")
                },
                new DentalTeamParticipant
                {
                    TeamId = new Guid("bfc227b5-eebc-4183-901d-29230fdda49a"),
                    ParticipantId = new Guid("3fc6d593-e172-433f-ac86-af578d6a81e8")
                },
                new DentalTeamParticipant
                {
                    TeamId = new Guid("451ecdbe-6ff4-49cb-a8b1-9862dba44f32"),
                    ParticipantId = new Guid("73b047e2-e5cd-456a-8c89-bb573dad0b43")
                },
                new DentalTeamParticipant
                {
                    TeamId = new Guid("bfc227b5-eebc-4183-901d-29230fdda49a"),
                    ParticipantId = new Guid("16e3487c-f39d-4bb0-ba5d-eec5db17ad5b")
                }
            };

        public Task<bool> InitData()
        {
            return Task.FromResult(true);
        }
    }
}