using Argent.Api.Infrastructure.Repositories;
using Argent.Api.Infrastructure.Repositories.Access;
using Argent.Api.Infrastructure.Repositories.Kyc;
using Argent.Api.Infrastructure.Repositories.Settings;

namespace Argent.Api.Infrastructure.Transactions {

    /// <summary>
    /// Single entry point for all repository access within a transaction.
    /// </summary>
    /// <remarks>
    /// Add a repository property here for each repository
    /// </remarks>
    public interface IUnitOfWork : IDisposable, IAsyncDisposable {
        //..repositories
         IOrganizationRepository Organizations { get; }
         IAccessRepository Access { get; }
         IAuditRepository Audits { get; }
         IConfigurationRepository Configs { get; }
        ICustomerRepository Customers { get; }

        Task<int> CommitAsync(CancellationToken token = default);
        Task RollbackAsync(CancellationToken token = default);
        Task CommitAuditAsync(CancellationToken ct = default);
        Task BeginTransactionAsync(CancellationToken token = default);
        Task<T> ExecuteInTransactionAsync<T>(Func<CancellationToken, Task<T>> operation, CancellationToken token = default);
    }
}
