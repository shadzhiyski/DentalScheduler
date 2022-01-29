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

        public TreatmentSessionReferencesBusinessValidator(
            IStringLocalizer<TreatmentSessionReferencesBusinessValidator> localizer,
            IReadRepository<DentalTeam> dentalTeamReadRepository)
        {
            RuleFor(m => m.DentalTeamReferenceId)
                .MustAsync((m, ctx, ct) => HasDentalTeam(m, ct))
                .WithMessage(localizer[InvalidDentalTeamMessageName]);

            DentalTeamReadRepository = dentalTeamReadRepository;
        }

        public IReadRepository<DentalTeam> DentalTeamReadRepository { get; }

        private Task<bool> HasDentalTeam(
            ITreatmentSessionReferencesInput model,
            CancellationToken cancellationToken)
            => DentalTeamReadRepository
                .Where(ts => ts.ReferenceId == model.DentalTeamReferenceId)
                .AnyAsync(cancellationToken);
    }
}