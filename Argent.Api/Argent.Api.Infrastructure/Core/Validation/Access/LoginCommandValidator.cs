using Argent.Api.Infrastructure.Core.Commands.Access;
using FluentValidation;

namespace Argent.Api.Infrastructure.Core.Validation.Access {
    public class LoginCommandValidator : AbstractValidator<LoginCommand> {
        public LoginCommandValidator() {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }
    }

}
