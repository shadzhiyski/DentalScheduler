using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Common.Dto.Output;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;
using DentalSystem.Application.Boundaries.UseCases.Common.Validation;
using System.Threading;
using MediatR;
using DentalSystem.Application.UseCases.Scheduling.Dto.Input;

namespace DentalSystem.Application.UseCases.Scheduling.Commands
{
    public class AddTreatmentSessionCommand : IRequestHandler<TreatmentSessionInput, IResult<IMessageOutput>>
    {
        public AddTreatmentSessionCommand(
            IApplicationValidator<ITreatmentSessionInput> validator,
            IWriteRepository<TreatmentSession> treatmentSessionRepository,
            IReadRepository<Patient> patientRepository,
            IReadRepository<DentalTeam> dentalTeamRepository,
            IReadRepository<Treatment> treatmentRepository,
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

        public IWriteRepository<TreatmentSession> TreatmentSessionRepository { get; }

        public IReadRepository<Patient> PatientRepository { get; }

        public IReadRepository<DentalTeam> DentalTeamRepository { get; }

        public IReadRepository<Treatment> TreatmentRepository { get; }

        public IUnitOfWork UoW { get; }

        public async Task<IResult<IMessageOutput>> Handle(TreatmentSessionInput input, CancellationToken cancellationToken)
        {
            var validationResult = Validator.Validate(input);
            if (validationResult.Errors.Count > 0)
            {
                return new Result<IMessageOutput>(validationResult.Errors);
            }

            var patient = await PatientRepository.SingleOrDefaultAsync(p => p.ReferenceId == input.PatientReferenceId, cancellationToken);
            var dentalTeam = await DentalTeamRepository.SingleOrDefaultAsync(dt => dt.ReferenceId == input.DentalTeamReferenceId, cancellationToken);
            var treatment = await TreatmentRepository.SingleOrDefaultAsync(t => t.ReferenceId == input.TreatmentReferenceId, cancellationToken);
            var treatmentSession = new TreatmentSession()
            {
                PatientId = patient.Id,
                DentalTeamId = dentalTeam.Id,
                TreatmentId = treatment.Id,
                Start = input.Start.Value,
                End = input.End.Value
            };

            await TreatmentSessionRepository.AddAsync(treatmentSession, cancellationToken);

            await UoW.SaveAsync(cancellationToken);

            return new Result<IMessageOutput>(
                value: new MessageOutput("Treatment Session successfully created."),
                type: ResultType.Created
            );
        }
    }
}