using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Infrastructure.Persistence;

namespace DentalScheduler.UseCases.Tests.Utilities.DataProviders
{
    public class DentalTeamDbDataProvider : IDentalTeamDbDataProvider
    {
        public DentalTeamDbDataProvider(
            IUserDbDataProvider userDbDataProvider,
            IRoomDbDataProvider roomDbDataProvider,
            IGenericRepository<DentalTeam> dentalTeamRepository,
            IGenericRepository<DentalWorker> dentistRepository,
            IGenericRepository<DentalTeamParticipant> dentalTeamParticipantRepository,
            IUnitOfWork uoW)
        {
            UserDbDataProvider = userDbDataProvider;
            RoomDbDataProvider = roomDbDataProvider;
            DentalTeamRepository = dentalTeamRepository;
            DentistRepository = dentistRepository;
            DentalTeamParticipantRepository = dentalTeamParticipantRepository;
            UoW = uoW;
        }

        public IUserDbDataProvider UserDbDataProvider { get; }

        public IRoomDbDataProvider RoomDbDataProvider { get; }
        
        public IGenericRepository<DentalTeam> DentalTeamRepository { get; }

        public IGenericRepository<DentalWorker> DentistRepository { get; }

        public IGenericRepository<DentalTeamParticipant> DentalTeamParticipantRepository { get; }

        public IUnitOfWork UoW { get; }
        
        public DentalTeam ProvideDentalTeam(string teamName, string roomName, params string[] dentistsUserNames)
        {
            var room = RoomDbDataProvider.ProvideRoom(roomName);
            var dentalTeam = new DentalTeam()
            {
                Name = teamName,
                RoomId = room.Id
            };

            DentalTeamRepository.Add(dentalTeam);
            UoW.Save();

            foreach (var dentistUserName in dentistsUserNames)
            {
                var (dentistUser, _) = UserDbDataProvider.ProvideDentist(dentistUserName, $"{dentistUserName}#123");
                var dentist = new DentalWorker
                {
                    IdentityUserId = dentistUser.Id.ToString()
                };

                DentistRepository.Add(dentist);
                UoW.Save();

                var dentalTeamParticipant = new DentalTeamParticipant
                {
                    ParticipantId = dentist.Id,
                    TeamId = dentalTeam.Id
                };

                DentalTeamParticipantRepository.Add(dentalTeamParticipant);
                UoW.Save();
            }

            return dentalTeam;
        }
    }
}