
using Argent.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Argent.Api.Infrastructure.Transactions {
    public class UnitOfWork(AppDataContext context) : IUnitOfWork
    {
        private readonly AppDataContext _context = context;
        private IDbContextTransaction? _transaction;
        private bool _disposed;

        public async Task BeginTransactionAsync(CancellationToken ct = default)
            => _transaction = await _context.Database.BeginTransactionAsync(ct);

        public async Task<int> CommitAsync(CancellationToken ct = default)
        {
            var result = await _context.SaveChangesAsync(ct);
            if (_transaction is not null)
            {
                await _transaction.CommitAsync(ct);
            }
            return result;
        }

        public async Task RollbackAsync(CancellationToken ct = default)
        {
            if (_transaction is not null)
            {
                await _transaction.RollbackAsync(ct);
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _transaction?.Dispose();
                _context.Dispose();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                if (_transaction is not null) await _transaction.DisposeAsync();
                await _context.DisposeAsync();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}
