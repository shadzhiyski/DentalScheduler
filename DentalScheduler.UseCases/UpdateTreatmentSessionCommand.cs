using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalScheduler.DTO.Output.Common;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Infrastructure.Persistence;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output.Common;
using DentalScheduler.Interfaces.UseCases.TreatmentSession;
using DentalScheduler.Interfaces.UseCases.Validation;
using DentalScheduler.UseCases.Validation;
using Microsoft.EntityFrameworkCore;

namespace DentalScheduler.UseCases
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

            if (!Enum.TryParse<TreatmentSessionStatus>(
                    value: input.Status, 
                    ignoreCase: false, 
                    out TreatmentSessionStatus treatmentSessionStatus))
            {
                validationResult.Errors.Add(new ValidationError()
                {
                    PropertyName = nameof(TreatmentSession.Status),
                    Errors = new List<string>
                    {
                        "Invalid treatment session status type."
                    }
                });

                return new Result<IMessageOutput>(validationResult.Errors);
            }

            var treatmentSession = await TreatmentSessionRepository.Where(
                    ts => ts.ReferenceId == input.ReferenceId
                ).Include(ts => ts.Treatment)
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
                    }
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
            treatmentSession.Status = treatmentSessionStatus;

            await UoW.SaveAsync();

            return new Result<IMessageOutput>(new MessageOutput("Treatment Session successfully updated."));
        }
    }
}