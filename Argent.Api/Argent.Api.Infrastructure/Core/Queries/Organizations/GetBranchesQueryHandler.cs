using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Queries.Organizations {
    /// <summary>
    /// Query handler to retrieve branches
    /// </summary>
    /// <param name="uow"></param>
    public class GetBranchesQueryHandler(IUnitOfWork uow)
        : IRequestHandler<GetBranchesQuery, Result<IEnumerable<BranchDto>>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<IEnumerable<BranchDto>>> Handle(GetBranchesQuery query, CancellationToken ct) {
            var branches = await _uow.Branches.GetAllAsync(ct);

            IEnumerable<BranchDto> dtos;
            if (!branches.Any()) {
                dtos = [];
                return Result<IEnumerable<BranchDto>>.Success(dtos);
            }
            
            dtos = branches.Select(b => new BranchDto {
                Id = b.Id,
                OrganizationId = b.OrganizationId,
                BranchCode = b.BranchCode,
                BranchName = b.BranchName,
                Address = b.Address,
                EmailAddress = b.EmailAddress,
                PostalAddress = b.PostalAddress,
                IsDefault = b.IsDefault,
                IsActive = b.IsActive,
                CreatedOn = b.CreatedOn
            });

            return Result<IEnumerable<BranchDto>>.Success(dtos);
        }
    }
}
