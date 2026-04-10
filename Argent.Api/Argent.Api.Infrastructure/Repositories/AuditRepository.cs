using Argent.Api.Domain.Entities.Audit;
using Argent.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Argent.Api.Infrastructure.Repositories {
    public class AuditRepository(AppDataContext context) : IAuditRepository {
        private readonly AppDataContext _context = context;

        public async Task AddAsync(AuditLog auditLog, CancellationToken ct = default)
            => await _context.AuditLogs.AddAsync(auditLog, ct);

        public async Task<IEnumerable<AuditLog>> GetByUserAsync(long userId, int page, int pageSize, CancellationToken token = default)
            => await _context.AuditLogs
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.OccurredOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);

        public async Task<IEnumerable<AuditLog>> GetByBranchAsync(long branchId, DateTime from, DateTime to, CancellationToken token = default)
            => await _context.AuditLogs
                .Where(a => a.BranchId == branchId && a.OccurredOn >= from && a.OccurredOn <= to)
                .OrderByDescending(a => a.OccurredOn)
                .ToListAsync(token);

        public async Task<IEnumerable<AuditLog>> GetByEntityAsync(string entityName, string entityId, CancellationToken token = default)
            => await _context.AuditLogs
                .Where(a => a.EntityName == entityName && a.EntityId == entityId)
                .OrderByDescending(a => a.OccurredOn)
                .ToListAsync(token);
    }
}
