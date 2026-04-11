
using Argent.Api.Infrastructure.Data;
using Argent.Api.Infrastructure.Repositories;
using Argent.Api.Infrastructure.Repositories.Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Argent.Api.Infrastructure.Transactions {
    public class UnitOfWork(AppDataContext context) : IUnitOfWork {
        private readonly AppDataContext _context = context;
        private IDbContextTransaction? _transaction;
        private bool _disposed;

        //..module repositories
        public IOrganizationRepository Organizations => new OrganizationRepository(_context);
        public IAccessRepository Access => new AccessRepository(_context);
        public IAuditRepository Audits => new AuditRepository(_context);

        public async Task BeginTransactionAsync(CancellationToken token = default)
            => _transaction = await _context.Database.BeginTransactionAsync(token);

        public async Task<int> CommitAsync(CancellationToken token = default) {
            var result = await _context.SaveChangesAsync(token);
            if (_transaction is not null) {
                await _transaction.CommitAsync(token);
            }
            return result;
        }

        public async Task RollbackAsync(CancellationToken token = default) {
            if (_transaction is not null) {
                await _transaction.RollbackAsync(token);
            }
        }

        public async Task CommitAuditAsync(CancellationToken token = default)
            => await _context.SaveChangesAsync(token);

        /// <summary>
        /// Use for multi-entity business operations
        /// </summary>
        /// <typeparam name="T">Transaction object type</typeparam>
        /// <param name="operation">Operation to handle</param>
        /// <param name="token">Cancellation token</param>
        /// <returns></returns>
        public async Task<T> ExecuteInTransactionAsync<T>(Func<CancellationToken, Task<T>> operation, CancellationToken token = default) {
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () => {
                await using var transaction = await _context.Database.BeginTransactionAsync(token);

                try {
                    var result = await operation(token);
                    await _context.SaveChangesAsync(token);
                    await transaction.CommitAsync(token);
                    return result;
                } catch {
                    await transaction.RollbackAsync(token);
                    throw;
                }
            });
        }

        public void Dispose() {
            if (!_disposed) {
                _transaction?.Dispose();
                _context.Dispose();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync() {
            if (!_disposed) {
                if (_transaction is not null) await _transaction.DisposeAsync();
                await _context.DisposeAsync();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}
