using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalScheduler.DTO.Output.Common;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Gateways;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output.Common;
using DentalScheduler.Interfaces.UseCases;
using DentalScheduler.Interfaces.UseCases.Validation;
using DentalScheduler.UseCases.Validation;

namespace DentalScheduler.UseCases
{
    public class UpdateTreatmentSessionCommand : IUpdateTreatmentSessionCommand
    {
        public UpdateTreatmentSessionCommand(
            IApplicationValidator<ITreatmentSessionInput> validator,
            IGenericRepository<TreatmentSession> treatmentSessionRepository,
            IUnitOfWork uoW)
        {
            Validator = validator;
            TreatmentSessionRepository = treatmentSessionRepository;
            UoW = uoW;
        }

        public IApplicationValidator<ITreatmentSessionInput> Validator { get; }
        
        public IGenericRepository<TreatmentSession> TreatmentSessionRepository { get; }

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

            var treatmentSession = TreatmentSessionRepository.SingleOrDefault(
                ts => ts.Patient.ReferenceId == input.PatientId
                    && ts.DentalTeam.ReferenceId == input.DentalTeamId
                    && ts.Start == input.Start
                    && ts.End == input.End
            );

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

            treatmentSession.Reason = input.Reason;
            treatmentSession.Status = treatmentSessionStatus;

            await UoW.SaveAsync();

            return new Result<IMessageOutput>(new MessageOutput("Treatment Session successfully updated."));
        }
    }
}