using FluentValidation;

namespace Argent.Api.Infrastructure.Core.Validation.Kyc {
    public class ApproveCustomerCommandValidator : AbstractValidator<ApproveCustomerCommand> {
        public ApproveCustomerCommandValidator() {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer ID is required.");
            RuleFor(x => x.TargetBranchId).NotEmpty().WithMessage("Branch is required.");
        }
    }


}
