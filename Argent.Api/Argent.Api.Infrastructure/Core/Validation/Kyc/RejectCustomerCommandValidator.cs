using FluentValidation;

namespace Argent.Api.Infrastructure.Core.Validation.Kyc {
    public class RejectCustomerCommandValidator : AbstractValidator<RejectCustomerCommand> {
        public RejectCustomerCommandValidator() {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer ID is required.");
            RuleFor(x => x.ReasonId).NotEmpty().WithMessage("A rejection reason is required.");
            RuleFor(x => x.TargetBranchId).NotEmpty().WithMessage("Branch is required.");
        }
    }


}
