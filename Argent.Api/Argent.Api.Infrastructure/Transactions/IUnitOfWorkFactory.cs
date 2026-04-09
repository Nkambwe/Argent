namespace Argent.Api.Infrastructure.Transactions {
    /// <summary>
    /// Factory for creating UnitOfWork instances. Inject this when you need
    /// to control transaction scope manually (e.g., in background services).
    /// For standard request-scoped usage, inject IUnitOfWork directly.
    /// </summary>
    public interface IUnitOfWorkFactory {
        IUnitOfWork Create();
    }

}
