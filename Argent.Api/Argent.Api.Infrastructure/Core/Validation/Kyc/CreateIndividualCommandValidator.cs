using Argent.Api.Infrastructure.Core.Commands.Kyc;
using Argent.Api.Infrastructure.Services;
using FluentValidation;

namespace Argent.Api.Infrastructure.Core.Validation.Kyc {

    /// <summary>
    /// Validates individual registration requests.
    /// </summary>
    /// <remarks>
    /// Required fields are driven by SystemConfig so the same validator works regardless of how the organization has configured their KYC rules. 
    /// </remarks>
    public class CreateIndividualCommandValidator : AbstractValidator<CreateIndividualCommand> {
        private readonly ISystemConfigurationService _config;

        public CreateIndividualCommandValidator(ISystemConfigurationService config) {
            _config = config;

            //..static rules — always enforced regardless of configuration
            RuleFor(x => x.Request.FirstName).NotEmpty().WithMessage("First name is required.").MaximumLength(100);
            RuleFor(x => x.Request.LastName).NotEmpty().WithMessage("Last name is required.").MaximumLength(100);
            RuleFor(x => x.TargetBranchId).NotEmpty().WithMessage("Branch is required.");

            // Dynamic rules — checked against SystemConfig
            RuleFor(x => x.Request.MiddleName).NotEmpty().WithMessage("Middle name is required.").When(_ => IsRequired("CustomerKyc.Individual", "RequireMiddleName"));
            RuleFor(x => x.Request.DateOfBirth).NotNull().WithMessage("Date of birth is required.").When(_ => IsRequired("CustomerKyc.Individual", "RequireDateOfBirth"));
            RuleFor(x => x.Request.DateOfBirth)
                .Must(dob => dob.HasValue && DateTime.UtcNow.Year - dob.Value.Year >= GetInt("CustomerKyc.Individual", "ClientMinimumAge", 18))
                .WithMessage(x => $"Customer must be at least {GetInt("CustomerKyc.Individual", "ClientMinimumAge", 18)} years old.")
                .When(x => x.Request.DateOfBirth.HasValue);

            RuleFor(x => x.Request.Mobile)
                .NotEmpty().WithMessage("Mobile number is required.")
                .When(_ => IsRequired("CustomerKyc", "RequireMobileNumber"));

            RuleFor(x => x.Request.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Valid email address is required.")
                .When(_ => IsRequired("CustomerKyc", "RequireCustomerEmail"));

            RuleFor(x => x.Request.PermanentAddress)
                .NotEmpty().WithMessage("Permanent address is required.")
                .When(_ => IsRequired("CustomerKyc", "RequirePermanentAddress"));

            RuleFor(x => x.Request.Photo)
                .NotEmpty().WithMessage("Customer photo is required.")
                .When(_ => IsRequired("CustomerKyc.Individual", "RequirePhoto"));

            RuleFor(x => x.Request.Contacts)
                .NotEmpty().WithMessage("At least one next of kin contact is required.")
                .When(_ => IsRequired("CustomerKyc.Individual", "RequireNextOfKin"));

            RuleFor(x => x.Request.NationalityId)
                .NotNull().WithMessage("Nationality is required.")
                .When(_ => IsRequired("CustomerKyc.Individual", "RequireNationality"));
        }

        // Synchronous config reads — values are cached so no DB hit per-call
        private bool IsRequired(string module, string key)
            => _config.GetBoolAsync(module, key, defaultValue: false)
                .GetAwaiter().GetResult();

        private int GetInt(string module, string key, int defaultValue)
            => _config.GetIntAsync(module, key, defaultValue)
                .GetAwaiter().GetResult();
    }


}
