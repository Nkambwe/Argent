using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries {
    public class GetGroupMembersQueryHandler(IUnitOfWork uow): IRequestHandler<GetGroupMembersQuery, Result<PagedResult<CustomerSummaryDto>>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<PagedResult<CustomerSummaryDto>>> Handle(GetGroupMembersQuery query, CancellationToken token) {
            var group = await _uow.Customers.GetGroupByIdAsync(query.GroupId, token);
            if (group is null)
                return Result<PagedResult<CustomerSummaryDto>>.NotFound("Group not found.");

            var (members, total) = await _uow.Customers.GetMembersByGroupAsync(query.GroupId, query.Active, query.Page, query.PageSize, token);
            var dtos = members.Select(m => new CustomerSummaryDto {
                Id = m.Id,
                ClientCode = m.ClientCode,
                CustomerType = CustomerType.Member,
                DisplayName = $"{m.FirstName} {m.LastName}".Trim(),
                Mobile = m.Mobile,
                Email = m.Email,
                BranchName = m.Branch?.BranchName ?? string.Empty,
                Active = m.Active,
                Approved = m.Approved,
                Exited = m.Exited,
                CanTransact = m.CanTransact,
                RegisteredOn = m.RegisteredOn
            });

            return Result<PagedResult<CustomerSummaryDto>>.Success(new PagedResult<CustomerSummaryDto> {
                Items = dtos,
                TotalCount = total,
                Page = query.Page,
                PageSize = query.PageSize
            });
        }
    }

}
