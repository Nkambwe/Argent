using Argent.Api.Infrastructure.Core.Commands.Organizations;
using FluentValidation;

namespace Argent.Api.Infrastructure.Core.Validation.Organization {
    public class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand> {
        public CreateOrganizationCommandValidator() {
            RuleFor(x => x.Request.RegisteredName)
                .NotEmpty().WithMessage("Registered name is required.")
                .MaximumLength(200).WithMessage("Registered name must not exceed 200 characters.");

            RuleFor(x => x.Request.ShortName)
                .NotEmpty().WithMessage("Short name is required.")
                .MaximumLength(50).WithMessage("Short name must not exceed 50 characters.");

            RuleFor(x => x.Request.RegistrationNumber)
                .NotEmpty().WithMessage("Registration number is required.")
                .MaximumLength(100).WithMessage("Registration number must not exceed 100 characters.");

            RuleFor(x => x.Request.BusinessLine)
                .NotEmpty().WithMessage("Business line is required.")
                .MaximumLength(100).WithMessage("Business line must not exceed 100 characters.");

            RuleFor(x => x.Request.ContactEmail)
                .NotEmpty().WithMessage("Contact email is required.")
                .EmailAddress().WithMessage("Contact email must be a valid email address.")
                .MaximumLength(150);

            //..default branch must be provided
            RuleFor(x => x.Request.DefaultBranch.BranchCode)
                .NotEmpty().WithMessage("Default branch code is required.")
                .MaximumLength(10);

            RuleFor(x => x.Request.DefaultBranch.BranchName)
               .NotEmpty().WithMessage("Default branch name is required.")
               .MaximumLength(150);

            RuleFor(x => x.Request.DefaultBranch.Address)
                .NotEmpty().WithMessage("Default branch address is required.")
                .MaximumLength(300);

            RuleFor(x => x.Request.DefaultBranch.EmailAddress)
                .NotEmpty().WithMessage("Default branch email is required.")
                .EmailAddress().WithMessage("Default branch email must be a valid email address.")
                .MaximumLength(150);

            RuleFor(x => x.Request.DefaultBranch.PostalAddress)
                .MaximumLength(100).When(x => x.Request.DefaultBranch.PostalAddress is not null);
        }
    }

}
