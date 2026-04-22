using FluentValidation;

namespace Argent.Api.Infrastructure.Core.Validation.Kyc {
    public class BlacklistCustomerCommandValidator : AbstractValidator<BlacklistCustomerCommand> {
        public BlacklistCustomerCommandValidator() {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer ID is required.");
            RuleFor(x => x.ReasonId).NotEmpty().WithMessage("A reason is required to blacklist a customer.");
            RuleFor(x => x.TargetBranchId).NotEmpty().WithMessage("Branch is required.");
        }
    }


}
