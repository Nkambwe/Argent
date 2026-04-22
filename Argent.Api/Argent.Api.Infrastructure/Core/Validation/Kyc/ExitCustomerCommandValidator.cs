using FluentValidation;

namespace Argent.Api.Infrastructure.Core.Validation.Kyc {
    public class ExitCustomerCommandValidator : AbstractValidator<ExitCustomerCommand> {
        public ExitCustomerCommandValidator() {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer ID is required.");
            RuleFor(x => x.ReasonId).NotEmpty().WithMessage("An exit reason is required.");
            RuleFor(x => x.TargetBranchId).NotEmpty().WithMessage("Branch is required.");
        }
    }


}
