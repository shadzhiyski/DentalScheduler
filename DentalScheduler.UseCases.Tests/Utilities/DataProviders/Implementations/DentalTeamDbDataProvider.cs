using System.Threading.Tasks;
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
        
        public async Task<DentalTeam> ProvideDentalTeamAsync(string teamName, string roomName, params string[] dentistsUserNames)
        {
            var room = await RoomDbDataProvider.ProvideRoomAsync(roomName);
            var dentalTeam = new DentalTeam()
            {
                Name = teamName,
                RoomId = room.Id
            };

            await DentalTeamRepository.AddAsync(dentalTeam);
            await UoW.SaveAsync();

            foreach (var dentistUserName in dentistsUserNames)
            {
                var (dentistUser, dentist) = await UserDbDataProvider.ProvideDentistAsync(dentistUserName, $"{dentistUserName}#123");

                var dentalTeamParticipant = new DentalTeamParticipant
                {
                    ParticipantId = dentist.Id,
                    TeamId = dentalTeam.Id
                };

                await DentalTeamParticipantRepository.AddAsync(dentalTeamParticipant);
                await UoW.SaveAsync();
            }

            return dentalTeam;
        }
    }
}