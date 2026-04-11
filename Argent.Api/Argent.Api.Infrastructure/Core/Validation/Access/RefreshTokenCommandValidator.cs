using Argent.Api.Infrastructure.Core.Commands.Access;
using FluentValidation;

namespace Argent.Api.Infrastructure.Core.Validation.Access {
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand> {
        public RefreshTokenCommandValidator() {
            RuleFor(x => x.AccessToken)
                .NotEmpty().WithMessage("Access token is required.");

            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required.");
        }
    }

}
