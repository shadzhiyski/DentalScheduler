using System.Threading;
using System.Threading.Tasks;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input;
using DentalSystem.Domain.Scheduling.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DentalSystem.Application.UseCases.Scheduling.Validation
{
    public class TreatmentSessionReferencesBusinessValidator : AbstractValidator<ITreatmentSessionReferencesInput>
    {
        public const string InvalidDentalTeamMessageName = "InvalidDentalTeam";
        public const string InvalidTreatmentMessageName = "InvalidTreatment";
        public const string InvalidPatientMessageName = "InvalidPatient";

        public TreatmentSessionReferencesBusinessValidator(
            IStringLocalizer<TreatmentSessionReferencesBusinessValidator> localizer,
            IReadRepository<DentalTeam> dentalTeamReadRepository,
            IReadRepository<Treatment> treatmentReadRepository,
            IReadRepository<Patient> patientReadRepository)
        {
            RuleFor(m => m.DentalTeamReferenceId)
                .MustAsync((m, ctx, ct) => HasDentalTeam(m, ct))
                .WithMessage(localizer[InvalidDentalTeamMessageName]);

            RuleFor(m => m.TreatmentReferenceId)
                .MustAsync((m, ctx, ct) => HasTreatment(m, ct))
                .WithMessage(localizer[InvalidTreatmentMessageName]);

            RuleFor(m => m.PatientReferenceId)
                .MustAsync((m, ctx, ct) => HasPatient(m, ct))
                .WithMessage(localizer[InvalidPatientMessageName]);

            DentalTeamReadRepository = dentalTeamReadRepository;
            TreatmentReadRepository = treatmentReadRepository;
            PatientReadRepository = patientReadRepository;
        }

        public IReadRepository<DentalTeam> DentalTeamReadRepository { get; }
        public IReadRepository<Treatment> TreatmentReadRepository { get; }
        public IReadRepository<Patient> PatientReadRepository { get; }

        private Task<bool> HasDentalTeam(
            ITreatmentSessionReferencesInput model,
            CancellationToken cancellationToken)
            => DentalTeamReadRepository
                .AsNoTracking()
                .AnyAsync(ts => ts.ReferenceId == model.DentalTeamReferenceId, cancellationToken);

        private Task<bool> HasTreatment(
            ITreatmentSessionReferencesInput model,
            CancellationToken cancellationToken)
            => TreatmentReadRepository
                .AsNoTracking()
                .AnyAsync(ts => ts.ReferenceId == model.TreatmentReferenceId, cancellationToken);

        private Task<bool> HasPatient(
            ITreatmentSessionReferencesInput model,
            CancellationToken cancellationToken)
            => PatientReadRepository
                .AsNoTracking()
                .AnyAsync(ts => ts.ReferenceId == model.PatientReferenceId, cancellationToken);
    }
}