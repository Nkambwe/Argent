using Argent.Api.Infrastructure.Core.Commands.Organization;
using FluentValidation;

namespace Argent.Api.Infrastructure.Core.Validation.Organization {
    public class UpdateOrganizationCommandValidator : AbstractValidator<UpdateOrganizationCommand> {
        public UpdateOrganizationCommandValidator() {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage("Organization ID is required.");

            RuleFor(x => x.RegisteredName)
                .NotEmpty().WithMessage("Registered name is required.")
                .MaximumLength(200);

            RuleFor(x => x.ShortName)
                .NotEmpty().WithMessage("Short name is required.")
                .MaximumLength(50);

            RuleFor(x => x.BusinessLine)
                .NotEmpty().WithMessage("Business line is required.")
                .MaximumLength(100);

            RuleFor(x => x.ContactEmail)
                .NotEmpty().WithMessage("Contact email is required.")
                .EmailAddress().WithMessage("Contact email must be a valid email address.")
                .MaximumLength(150);
        }
    }

}
