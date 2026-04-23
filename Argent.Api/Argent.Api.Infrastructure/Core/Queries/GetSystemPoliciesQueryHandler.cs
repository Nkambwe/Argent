using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Argent.Api.Infrastructure.Core.Queries {
    public class GetSystemPoliciesQueryHandler(AppDataContext db)
                : IRequestHandler<GetSystemPoliciesQuery, Result<IEnumerable<SystemPolicyDto>>> {
        private readonly AppDataContext _db = db;

        public async Task<Result<IEnumerable<SystemPolicyDto>>> Handle(
            GetSystemPoliciesQuery query, CancellationToken ct) {
            var dbQuery = _db.SystemPolicies
                .Where(p => !p.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Module))
                dbQuery = dbQuery.Where(p => p.Module == query.Module);

            var policies = await dbQuery
                .OrderBy(p => p.Module)
                .ThenBy(p => p.Name)
                .ToListAsync(ct);

            var dtos = policies.Select(p => new SystemPolicyDto
            {
                Id = p.Id,
                Name = p.Name,
                Module = p.Module,
                Description = p.Description,
                DefaultValue = p.DefaultValue,
                DataType = p.DataType.ToString(),
                IsOverridable = p.IsOverridable
            });

            return Result<IEnumerable<SystemPolicyDto>>.Success(dtos);
        }
    }
}
