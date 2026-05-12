using Argent.Api.Infrastructure.Core.Commands.Organizations;
using FluentValidation;

namespace Argent.Api.Infrastructure.Core.Validation.Organization {
    public class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand> {
        public CreateBranchCommandValidator() {
            RuleFor(x => x.Request.OrganizationId)
                .NotEmpty().WithMessage("Organization ID is required.");

            RuleFor(x => x.Request.BranchCode)
                .NotEmpty().WithMessage("Branch code is required.")
                .MaximumLength(10);

            RuleFor(x => x.Request.BranchName)
                .NotEmpty().WithMessage("Branch name is required.")
                .MaximumLength(150);

            RuleFor(x => x.Request.Address)
                .NotEmpty().WithMessage("Branch address is required.")
                .MaximumLength(300);

            RuleFor(x => x.Request.EmailAddress)
                .NotEmpty().WithMessage("Branch email is required.")
                .EmailAddress().WithMessage("Branch email must be a valid email address.")
                .MaximumLength(150);

            RuleFor(x => x.Request.PostalAddress)
                .MaximumLength(100).When(x => x.Request.PostalAddress is not null);
        }
    }

}
