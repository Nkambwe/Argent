using Argent.Api.Infrastructure.Core.Commands.Organization;
using FluentValidation;

namespace Argent.Api.Infrastructure.Core.Validation {
    public class SetDefaultBranchCommandValidator : AbstractValidator<SetDefaultBranchCommand> {
        public SetDefaultBranchCommandValidator() {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage("Organization ID is required.");

            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("Branch ID is required.");
        }
    }

}
