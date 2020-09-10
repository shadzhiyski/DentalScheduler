using System.Threading.Tasks;
using DentalScheduler.DTO.Output.Common;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Gateways;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output.Common;
using DentalScheduler.Interfaces.UseCases;
using DentalScheduler.Interfaces.UseCases.Validation;

namespace DentalScheduler.UseCases
{
    public class AddTreatmentSessionCommand : IAddTreatmentSessionCommand
    {
        public AddTreatmentSessionCommand(
            IApplicationValidator<ITreatmentSessionInput> validator,
            IGenericRepository<TreatmentSession> treatmentSessionRepository,
            IGenericRepository<Patient> patientRepository,
            IGenericRepository<DentalTeam> dentalTeamRepository,
            IGenericRepository<Treatment> treatmentRepository,
            IUnitOfWork uoW)
        {
            Validator = validator;
            TreatmentSessionRepository = treatmentSessionRepository;
            PatientRepository = patientRepository;
            DentalTeamRepository = dentalTeamRepository;
            TreatmentRepository = treatmentRepository;
            UoW = uoW;
        }

        public IApplicationValidator<ITreatmentSessionInput> Validator { get; }
        
        public IGenericRepository<TreatmentSession> TreatmentSessionRepository { get; }

        public IGenericRepository<Patient> PatientRepository { get; }
        
        public IGenericRepository<DentalTeam> DentalTeamRepository { get; }

        public IGenericRepository<Treatment> TreatmentRepository { get; }

        public IUnitOfWork UoW { get; }

        public async Task<IResult<IMessageOutput>> ExecuteAsync(ITreatmentSessionInput input)
        {
            var validationResult = Validator.Validate(input);
            if (validationResult.Errors.Count > 0)
            {
                return new Result<IMessageOutput>(validationResult.Errors);
            }

            var patient = PatientRepository.SingleOrDefault(p => p.ReferenceId == input.PatientId);
            var dentalTeam = DentalTeamRepository.SingleOrDefault(dt => dt.ReferenceId == input.DentalTeamReferenceId);
            var treatment = TreatmentRepository.SingleOrDefault(t => t.ReferenceId == input.TreatmentReferenceId);
            var treatmentSession = new TreatmentSession()
            {
                PatientId = patient.Id,
                DentalTeamId = dentalTeam.Id,
                TreatmentId = treatment.Id,
                Reason = input.Reason,
                Start = input.Start.Value,
                End = input.End.Value
            };

            TreatmentSessionRepository.Add(treatmentSession);

            await UoW.SaveAsync();

            return new Result<IMessageOutput>(new MessageOutput("Treatment Session successfully added."));
        }
    }
}