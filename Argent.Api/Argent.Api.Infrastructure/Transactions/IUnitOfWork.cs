namespace Argent.Api.Infrastructure.Transactions {
    public interface IUnitOfWork : IDisposable, IAsyncDisposable {
        //..repositories
        // IOrganizationRepository Organizations { get; }
        // ICustomerRepository Customers { get; }

        Task<int> CommitAsync(CancellationToken ct = default);
        Task RollbackAsync(CancellationToken ct = default);
        Task BeginTransactionAsync(CancellationToken ct = default);
    }
}
