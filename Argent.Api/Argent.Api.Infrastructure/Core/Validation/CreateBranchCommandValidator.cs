using Argent.Api.Infrastructure.Core.Commands.Organization;
using FluentValidation;

namespace Argent.Api.Infrastructure.Core.Validation {
    public class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand> {
        public CreateBranchCommandValidator() {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage("Organization ID is required.");

            RuleFor(x => x.BranchName)
                .NotEmpty().WithMessage("Branch name is required.")
                .MaximumLength(150);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Branch address is required.")
                .MaximumLength(300);

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Branch email is required.")
                .EmailAddress().WithMessage("Branch email must be a valid email address.")
                .MaximumLength(150);

            RuleFor(x => x.PostalAddress)
                .MaximumLength(100).When(x => x.PostalAddress is not null);
        }
    }

}
