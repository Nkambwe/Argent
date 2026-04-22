using Argent.Api.Infrastructure.Core.Commands.Kyc;
using FluentValidation;

namespace Argent.Api.Infrastructure.Core.Validation.Kyc {
    public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand> {
        public CreateMemberCommandValidator() {
            RuleFor(x => x.Request.GroupId)
                .NotEmpty().WithMessage("Group is required.");

            RuleFor(x => x.Request.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Request.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100);

            RuleFor(x => x.TargetBranchId)
                .NotEmpty().WithMessage("Branch is required.");
        }
    }


}
