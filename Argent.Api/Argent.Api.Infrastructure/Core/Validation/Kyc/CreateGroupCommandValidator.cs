using Argent.Api.Infrastructure.Core.Commands.Kyc;
using FluentValidation;

namespace Argent.Api.Infrastructure.Core.Validation.Kyc {
    public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand> {
        public CreateGroupCommandValidator() {
            RuleFor(x => x.Request.RegisteredName)
                .NotEmpty().WithMessage("Group name is required.")
                .MaximumLength(200);

            RuleFor(x => x.TargetBranchId)
                .NotEmpty().WithMessage("Branch is required.");

            RuleFor(x => x.Request.Mobile)
                .Matches(@"^\+?[0-9\s\-()]+$")
                .WithMessage("Invalid mobile number format.")
                .When(x => !string.IsNullOrEmpty(x.Request.Mobile));

            RuleFor(x => x.Request.Email)
                .EmailAddress().WithMessage("Valid email address is required.")
                .When(x => !string.IsNullOrEmpty(x.Request.Email));
        }
    }


}
