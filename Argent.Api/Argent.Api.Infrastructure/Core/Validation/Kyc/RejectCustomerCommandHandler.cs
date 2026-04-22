using Argent.Api.Domain.Entities.Kyc;
using Argent.Api.Domain.Entities.Support.KycSupport;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Validation.Kyc {
    public class RejectCustomerCommandHandler(IUnitOfWork uow, IUserContext userContext) : IRequestHandler<RejectCustomerCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result> Handle(RejectCustomerCommand command, CancellationToken token) {
            var customer = await _uow.Customers.GetCustomerBaseAsync(
                command.CustomerId, command.CustomerType, token);

            if (customer is null) return Result.Failure("Customer not found.", "NOT_FOUND");
            if (customer.Approved) return Result.Failure("Cannot reject an already approved customer.", "ALREADY_APPROVED");

            return await _uow.ExecuteInTransactionAsync(async token =>
            {
                await _uow.Customers.AddApprovalAsync(new CustomerApproval
                {
                    CustomerId = customer.Id,
                    CustomerType = command.CustomerType,
                    Status = ApprovalStatus.Rejected,
                    ActionedOn = DateTime.UtcNow,
                    ActionedBy = _userContext.Username,
                    Comments = command.Notes
                }, token);

                await _uow.Customers.AddRejectionAsync(new RejectedCustomer
                {
                    CustomerId = customer.Id,
                    CustomerType = command.CustomerType,
                    RejectDate = DateTime.UtcNow,
                    RejectedBy = _userContext.Username,
                    ReasonId = command.ReasonId,
                    Notes = command.Notes
                }, token);

                return Result.Success();
            }, token);
        }
    }


}
