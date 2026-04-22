using Argent.Api.Domain.Entities.Kyc;
using Argent.Api.Domain.Entities.Kyc.KycBusinesses;
using Argent.Api.Domain.Entities.Kyc.KycGroups;
using Argent.Api.Domain.Entities.Kyc.KycIndividuals;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects;
using Argent.Api.Infrastructure.Services;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Validation.Kyc {
    public class ApproveCustomerCommandHandler
        : IRequestHandler<ApproveCustomerCommand, Result<CustomerSummaryDto>> {
        private readonly IUnitOfWork _uow;
        private readonly ISystemConfigurationService _config;
        private readonly IUserContext _userContext;

        public ApproveCustomerCommandHandler(
            IUnitOfWork uow, ISystemConfigurationService config, IUserContext userContext) {
            _uow = uow; _config = config; _userContext = userContext;
        }

        public async Task<Result<CustomerSummaryDto>> Handle(ApproveCustomerCommand command, CancellationToken token) {
            var customer = await _uow.Customers.GetCustomerBaseAsync(
                command.CustomerId, command.CustomerType, token);

            if (customer is null)
                return Result<CustomerSummaryDto>.NotFound("Customer not found.");

            if (customer.Approved)
                return Result<CustomerSummaryDto>.Failure("Customer is already approved.", "ALREADY_APPROVED");

            if (customer.Exited)
                return Result<CustomerSummaryDto>.Failure("Cannot approve an exited customer.", "CUSTOMER_EXITED");

            // CanApproveOwnRegistrations check
            var canApproveOwn = await _config.GetBoolAsync("CustomerKyc", "CanApproveOwnRegistrations", defaultValue: false, token: token);

            if (!canApproveOwn && customer.CreatedBy == _userContext.Username)
                return Result<CustomerSummaryDto>.Failure("You cannot approve a customer you registered. A different user must approve.", "SELF_APPROVAL_DENIED");

            return await _uow.ExecuteInTransactionAsync(async token =>
            {
                // Activate and approve
                customer.Approved = true;
                customer.Active = true;
                customer.ApprovedOn = DateTime.UtcNow;
                customer.ApprovedBy = _userContext.Username;
                customer.CanTransact = true;

                // Update the correct table via type-specific update
                switch (command.CustomerType) {
                    case CustomerType.Individual:
                        _uow.Customers.UpdateIndividual((Individual)customer);
                        break;
                    case CustomerType.Member:
                        _uow.Customers.UpdateMember((Member)customer);
                        break;
                    case CustomerType.Group:
                        _uow.Customers.UpdateGroup((Group)customer);
                        break;
                    case CustomerType.Business:
                        _uow.Customers.UpdateBusiness((Business)customer);
                        break;
                }

                // Record approval
                await _uow.Customers.AddApprovalAsync(new CustomerApproval
                {
                    CustomerId = customer.Id,
                    CustomerType = command.CustomerType,
                    Status = ApprovalStatus.Approved,
                    ActionedOn = DateTime.UtcNow,
                    ActionedBy = _userContext.Username,
                    Comments = command.Comments
                }, token);

                return Result<CustomerSummaryDto>.Success(new CustomerSummaryDto
                {
                    Id = customer.Id,
                    ClientCode = customer.ClientCode,
                    CustomerType = command.CustomerType,
                    DisplayName = GetDisplayName(customer, command.CustomerType),
                    Mobile = customer.Mobile,
                    BranchName = customer.Branch?.BranchName ?? string.Empty,
                    Active = customer.Active,
                    Approved = customer.Approved,
                    Exited = customer.Exited,
                    CanTransact = customer.CanTransact,
                    RegisteredOn = customer.RegisteredOn
                });
            }, token);
        }

        private static string GetDisplayName(CustomerBase c, CustomerType type) => type switch
        {
            CustomerType.Individual => $"{((Individual)c).FirstName} {((Individual)c).LastName}".Trim(),
            CustomerType.Member => $"{((Member)c).FirstName} {((Member)c).LastName}".Trim(),
            CustomerType.Group => ((Group)c).RegisteredName,
            CustomerType.Business => ((Business)c).LegalName,
            _ => c.ClientCode
        };
    }


}
