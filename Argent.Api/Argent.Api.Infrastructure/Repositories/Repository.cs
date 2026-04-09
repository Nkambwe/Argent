
using Argent.Api.Domain.Entities;
using Argent.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Argent.Api.Infrastructure.Repositories
{
    public class Repository<T>(AppDataContext context) : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDataContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<T?> GetByIdAsync(long id, CancellationToken ct = default)
            => await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted, ct);

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
            => await _dbSet.Where(e => !e.IsDeleted).ToListAsync(ct);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
            => await _dbSet.Where(predicate).Where(e => !e.IsDeleted).ToListAsync(ct);

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
            => await _dbSet.Where(e => !e.IsDeleted).FirstOrDefaultAsync(predicate, ct);

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
            => await _dbSet.Where(e => !e.IsDeleted).AnyAsync(predicate, ct);

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default) {
            var query = _dbSet.Where(e => !e.IsDeleted);
            return predicate is null
                ? await query.CountAsync(ct)
                : await query.CountAsync(predicate, ct);
        }

        public async Task AddAsync(T entity, CancellationToken ct = default)
            => await _dbSet.AddAsync(entity, ct);

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
            => await _dbSet.AddRangeAsync(entities, ct);

        public void Update(T entity) {
            entity.UpdatedOn = DateTime.Now;
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
            => _dbSet.Remove(entity);

        public void SoftDelete(T entity, string? deletedBy = null) {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            entity.DeletedBy = deletedBy;
            _dbSet.Update(entity);
        }
    }
}
