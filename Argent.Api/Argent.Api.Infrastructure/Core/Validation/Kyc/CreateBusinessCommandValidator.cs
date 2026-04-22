using Argent.Api.Infrastructure.Core.Commands.Kyc;
using FluentValidation;

namespace Argent.Api.Infrastructure.Core.Validation.Kyc {
    public class CreateBusinessCommandValidator : AbstractValidator<CreateBusinessCommand> {
        public CreateBusinessCommandValidator() {
            RuleFor(x => x.Request.LegalName)
                .NotEmpty().WithMessage("Business legal name is required.")
                .MaximumLength(200);

            RuleFor(x => x.TargetBranchId)
                .NotEmpty().WithMessage("Branch is required.");

            RuleFor(x => x.Request.Email)
                .EmailAddress().WithMessage("Valid email address is required.")
                .When(x => !string.IsNullOrEmpty(x.Request.Email));
        }
    }


}
