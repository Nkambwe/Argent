using Argent.Api.Domain.Entities;
using Argent.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Argent.Api.Infrastructure.Repositories {
    public class Repository<T>(AppDataContext context) : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDataContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<T?> GetByIdAsync(long id, CancellationToken token = default)
            => await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted, token);

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
            => await _dbSet.Where(e => !e.IsDeleted).ToListAsync(token);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default)
            => await _dbSet.Where(predicate).Where(e => !e.IsDeleted).ToListAsync(token);

        public async Task<IEnumerable<T?>> FindAsync(Expression<Func<T, bool>> where, CancellationToken token = default, 
            params Expression<Func<T, object>>[] includes) {
            IQueryable<T> query = _dbSet;

            if (includes != null && includes.Length > 0) {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.Where(where).ToListAsync(token);
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default)
            => await _dbSet.Where(e => !e.IsDeleted).FirstOrDefaultAsync(predicate, token);

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default, params Expression<Func<T, object>>[] includes) {
            IQueryable<T> query = _dbSet;

            if (includes?.Length > 0) {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.FirstOrDefaultAsync(predicate, token);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default)
            => await _dbSet.Where(e => !e.IsDeleted).AnyAsync(predicate, token);

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken token = default) {
            var query = _dbSet.Where(e => !e.IsDeleted);
            return predicate is null
                ? await query.CountAsync(token)
                : await query.CountAsync(predicate, token);
        }

        public async Task AddAsync(T entity, CancellationToken token = default)
            => await _dbSet.AddAsync(entity, token);

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken token = default)
            => await _dbSet.AddRangeAsync(entities, token);

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
