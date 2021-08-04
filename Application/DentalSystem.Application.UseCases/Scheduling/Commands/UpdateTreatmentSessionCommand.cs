using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Common.Dto.Output;
using DentalSystem.Domain.Scheduling.Entities;
using DentalSystem.Domain.Scheduling.Enumerations;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;
using DentalSystem.Application.Boundaries.UseCases.Common.Validation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using MediatR;
using DentalSystem.Application.UseCases.Scheduling.Dto.Input;

namespace DentalSystem.Application.UseCases.Scheduling.Commands
{
    public class UpdateTreatmentSessionCommand : IRequestHandler<UpdateTreatmentSessionInput, IResult<IMessageOutput>>
    {
        public UpdateTreatmentSessionCommand(
            IApplicationValidator<IUpdateTreatmentSessionInput> validator,
            IReadRepository<TreatmentSession> treatmentSessionRepository,
            IReadRepository<Treatment> treatmentRepository,
            IReadRepository<DentalTeam> dentalTeamRepository,
            IUnitOfWork uoW)
        {
            Validator = validator;
            TreatmentSessionRepository = treatmentSessionRepository;
            TreatmentRepository = treatmentRepository;
            DentalTeamRepository = dentalTeamRepository;
            UoW = uoW;
        }

        public IApplicationValidator<IUpdateTreatmentSessionInput> Validator { get; }

        public IReadRepository<TreatmentSession> TreatmentSessionRepository { get; }

        public IReadRepository<Treatment> TreatmentRepository { get; }

        public IReadRepository<DentalTeam> DentalTeamRepository { get; }

        public IUnitOfWork UoW { get; }

        public async Task<IResult<IMessageOutput>> Handle(UpdateTreatmentSessionInput input, CancellationToken cancellationToken)
        {
            var validationResult = Validator.Validate(input);
            if (validationResult.Errors.Count > 0)
            {
                return new Result<IMessageOutput>(validationResult.Errors);
            }

            var treatmentSession = await TreatmentSessionRepository.Where(
                    ts => ts.ReferenceId == input.ReferenceId
                )
                .Include(ts => ts.Treatment)
                .Include(ts => ts.DentalTeam)
                .SingleOrDefaultAsync(cancellationToken);

            if (treatmentSession.Treatment.ReferenceId != input.TreatmentReferenceId)
            {
                var treatment = await TreatmentRepository.SingleOrDefaultAsync(
                    t => t.ReferenceId == input.TreatmentReferenceId,
                    cancellationToken
                );

                treatmentSession.TreatmentId = treatment.Id;
            }

            if (treatmentSession.DentalTeam.ReferenceId != input.DentalTeamReferenceId)
            {
                var dentalTeam = await DentalTeamRepository.SingleOrDefaultAsync(
                    dt => dt.ReferenceId == input.DentalTeamReferenceId,
                    cancellationToken
                );

                treatmentSession.DentalTeamId = dentalTeam.Id;
            }

            treatmentSession.Start = input.Start.Value;
            treatmentSession.End = input.End.Value;
            treatmentSession.Status = Enum.Parse<TreatmentSessionStatus>(input.Status);

            await UoW.SaveAsync(cancellationToken);

            return new Result<IMessageOutput>(
                value: new MessageOutput("Treatment Session successfully updated."),
                type: ResultType.Updated
            );
        }
    }
}