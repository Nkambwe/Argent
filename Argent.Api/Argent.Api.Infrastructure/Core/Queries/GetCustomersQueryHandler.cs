using Argent.Api.Domain.Entities.Kyc;
using Argent.Api.Domain.Entities.Kyc.KycBusinesses;
using Argent.Api.Domain.Entities.Kyc.KycGroups;
using Argent.Api.Domain.Entities.Kyc.KycIndividuals;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries {
    public class GetCustomersQueryHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<GetCustomersQuery, Result<PagedResult<CustomerSummaryDto>>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<PagedResult<CustomerSummaryDto>>> Handle(GetCustomersQuery query, CancellationToken token) {
            // Scope to user's accessible branches unless a specific branch is requested
            var branchId = query.BranchId;
            if (branchId.HasValue && !_userContext.CanAccessBranch(branchId.Value))
                return Result<PagedResult<CustomerSummaryDto>>.Failure("You do not have access to the specified branch.", "BRANCH_ACCESS_DENIED");

            IEnumerable<CustomerBase> rawItems;
            int total;

            switch (query.CustomerType) {
                case CustomerType.Individual: {
                        var (items, count) = await _uow.Customers.GetIndividualsAsync(
                            branchId, query.Active, query.Approved,
                            query.Search, query.Page, query.PageSize, token);
                        rawItems = items;
                        total = count;
                        break;
                    }
                case CustomerType.Group: {
                        var (items, count) = await _uow.Customers.GetGroupsAsync(
                            branchId, query.Active, query.Approved,
                            query.Search, query.Page, query.PageSize, token);
                        rawItems = items;
                        total = count;
                        break;
                    }
                case CustomerType.Business: {
                        var (items, count) = await _uow.Customers.GetBusinessesAsync(
                            branchId, query.Active, query.Approved,
                            query.Search, query.Page, query.PageSize, token);
                        rawItems = items;
                        total = count;
                        break;
                    }
                default:
                    return Result<PagedResult<CustomerSummaryDto>>.Failure($"Unsupported customer type: {query.CustomerType}", "UNSUPPORTED_TYPE");
            }

            var dtos = rawItems.Select(c => new CustomerSummaryDto {
                Id = c.Id,
                ClientCode = c.ClientCode,
                CustomerType = query.CustomerType,
                DisplayName = GetDisplayName(c, query.CustomerType),
                Mobile = c.Mobile,
                Email = c.Email,
                BranchName = c.Branch?.BranchName ?? string.Empty,
                Active = c.Active,
                Approved = c.Approved,
                Exited = c.Exited,
                CanTransact = c.CanTransact,
                RegisteredOn = c.RegisteredOn
            });

            return Result<PagedResult<CustomerSummaryDto>>.Success(new PagedResult<CustomerSummaryDto>
            {
                Items = dtos,
                TotalCount = total,
                Page = query.Page,
                PageSize = query.PageSize
            });
        }

        private static string GetDisplayName(CustomerBase c, CustomerType type) => type switch {
            CustomerType.Individual => $"{((Individual)c).FirstName} {((Individual)c).LastName}".Trim(),
            CustomerType.Member => $"{((Member)c).FirstName} {((Member)c).LastName}".Trim(),
            CustomerType.Group => ((Group)c).RegisteredName,
            CustomerType.Business => ((Business)c).LegalName,
            _ => c.ClientCode
        };
    }

}
