using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalScheduler.UseCases.Common.Dto.Output;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Infrastructure.Persistence;
using DentalScheduler.Interfaces.UseCases.Scheduling.Dto.Input;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;
using DentalScheduler.Interfaces.UseCases.Scheduling;
using DentalScheduler.Interfaces.UseCases.Common.Validation;
using Microsoft.EntityFrameworkCore;

namespace DentalScheduler.UseCases.Scheduling
{
    public class UpdateTreatmentSessionCommand : IUpdateTreatmentSessionCommand
    {
        public UpdateTreatmentSessionCommand(
            IApplicationValidator<ITreatmentSessionInput> validator,
            IGenericRepository<TreatmentSession> treatmentSessionRepository,
            IGenericRepository<Treatment> treatmentRepository,
            IGenericRepository<DentalTeam> dentalTeamRepository,
            IUnitOfWork uoW)
        {
            Validator = validator;
            TreatmentSessionRepository = treatmentSessionRepository;
            TreatmentRepository = treatmentRepository;
            DentalTeamRepository = dentalTeamRepository;
            UoW = uoW;
        }

        public IApplicationValidator<ITreatmentSessionInput> Validator { get; }
        
        public IGenericRepository<TreatmentSession> TreatmentSessionRepository { get; }

        public IGenericRepository<Treatment> TreatmentRepository { get; }

        public IGenericRepository<DentalTeam> DentalTeamRepository { get; }
        public IUnitOfWork UoW { get; }

        public async Task<IResult<IMessageOutput>> ExecuteAsync(ITreatmentSessionInput input)
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
                .SingleOrDefaultAsync();

            if (treatmentSession == null)
            {
                return new Result<IMessageOutput>(
                    new List<IError> 
                    { 
                        new Error(
                            ErrorType.NotFound, 
                            $"Treatment session for the given Patient, Dental Team and period cannot be found."
                        )
                    },
                    ResultStatus.NotFound
                );
            }
            
            if (treatmentSession.Treatment.ReferenceId != input.TreatmentReferenceId)
            {
                var treatment = TreatmentRepository.SingleOrDefault(
                    t => t.ReferenceId == input.TreatmentReferenceId
                );

                treatmentSession.TreatmentId = treatment.Id;
            }

            if (treatmentSession.DentalTeam.ReferenceId != input.DentalTeamReferenceId)
            {
                var dentalTeam = DentalTeamRepository.SingleOrDefault(
                    dt => dt.ReferenceId == input.DentalTeamReferenceId
                );

                treatmentSession.DentalTeamId = dentalTeam.Id;
            }

            treatmentSession.Start = input.Start.Value;
            treatmentSession.End = input.End.Value;
            treatmentSession.Status = Enum.Parse<TreatmentSessionStatus>(input.Status);

            await UoW.SaveAsync();

            return new Result<IMessageOutput>(new MessageOutput("Treatment Session successfully updated."));
        }
    }
}