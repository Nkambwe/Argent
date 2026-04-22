using Argent.Api.Domain.Entities.Kyc;
using Argent.Api.Domain.Entities.Kyc.KycBusinesses;
using Argent.Api.Domain.Entities.Kyc.KycGroups;
using Argent.Api.Domain.Entities.Kyc.KycIndividuals;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Validation.Kyc {
    public class BlacklistCustomerCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<BlacklistCustomerCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result> Handle(BlacklistCustomerCommand command, CancellationToken token) {
            var customer = await _uow.Customers.GetCustomerBaseAsync(
                command.CustomerId, command.CustomerType, token);

            if (customer is null)
                return Result.Failure("Customer not found.", "NOT_FOUND");

            var alreadyListed = await _uow.Customers.IsBlacklistedAsync(
                command.CustomerId, command.CustomerType, token);
            if (alreadyListed)
                return Result.Failure("Customer is already blacklisted.", "ALREADY_BLACKLISTED");

            return await _uow.ExecuteInTransactionAsync(async token =>
            {
                // Suspend transacting capability
                customer.CanTransact = false;
                switch (command.CustomerType) {
                    case CustomerType.Individual: _uow.Customers.UpdateIndividual((Individual)customer); break;
                    case CustomerType.Member: _uow.Customers.UpdateMember((Member)customer); break;
                    case CustomerType.Group: _uow.Customers.UpdateGroup((Group)customer); break;
                    case CustomerType.Business: _uow.Customers.UpdateBusiness((Business)customer); break;
                }

                await _uow.Customers.AddBlacklistAsync(new CustomerBlackList
                {
                    CustomerId = customer.Id,
                    CustomerType = command.CustomerType,
                    ListedOn = DateTime.UtcNow,
                    ReasonId = command.ReasonId,
                    Notes = command.Notes
                }, token);

                return Result.Success();
            }, token);
        }
    }


}
