using Argent.Api.Domain.Entities;
using System.Linq.Expressions;

namespace Argent.Api.Infrastructure.Repositories {
    public interface IRepository<T> where T: BaseEntity {

        Task<T?> GetByIdAsync(long id, CancellationToken token = default);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default);
        Task<IEnumerable<T?>> FindAsync(Expression<Func<T, bool>> where, CancellationToken token = default, params Expression<Func<T, object>>[] includes);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken token = default);
        Task AddAsync(T entity, CancellationToken token = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken token = default);
        void Update(T entity);
        void Remove(T entity);
        void SoftDelete(T entity, string? deletedBy = null);
    }
}
