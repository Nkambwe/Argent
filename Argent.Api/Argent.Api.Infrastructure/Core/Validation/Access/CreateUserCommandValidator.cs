using Argent.Api.Infrastructure.Core.Commands.Access;
using FluentValidation;

namespace Argent.Api.Infrastructure.Core.Validation.Access {
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand> {
        public CreateUserCommandValidator() {
            RuleFor(x => x.Request.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.")
                .Matches("^[a-zA-Z0-9._-]+$")
                .WithMessage("Username may only contain letters, numbers, dots, underscores, or hyphens.");

            RuleFor(x => x.Request.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.")
                .MaximumLength(150);

            RuleFor(x => x.Request.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.Request.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Request.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Request.PhoneNumber)
                .MaximumLength(20)
                .Matches(@"^\+?[0-9\s\-()]+$")
                .WithMessage("Phone number format is invalid.")
                .When(x => !string.IsNullOrEmpty(x.Request.PhoneNumber));

            RuleFor(x => x.Request.DefualtBranchId)
                .NotEmpty().WithMessage("Home branch is required.");

            RuleFor(x => x.Request.RoleIds)
                .NotEmpty().WithMessage("At least one role must be assigned.");
        }
    }

}
