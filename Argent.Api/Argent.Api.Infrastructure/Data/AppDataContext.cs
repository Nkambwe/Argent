using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities;
using Argent.Api.Domain.Entities.Access;
using Argent.Api.Domain.Entities.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Argent.Api.Infrastructure.Data {
    public class AppDataContext(DbContextOptions<AppDataContext> options) : DbContext(options) {

        //..organization objects
        public DbSet<Organization> Organizations => Set<Organization>();
        public DbSet<Branch> Branches => Set<Branch>();

        //..system access objects
        public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<UserBranchAccess> UserBranchAccess => Set<UserBranchAccess>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        //..audit objects
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            //..apply all entity configurations from this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDataContext).Assembly);

            //..default schema — all tables live under the "mfi" schema in PostgreSQL
            modelBuilder.HasDefaultSchema("mfi");

            //..all entities with soft delete are filtered automatically
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType)) {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var body = Expression.Equal(Expression.Property(parameter, "IsDeleted"), Expression.Constant(false));
                    var lambda = Expression.Lambda(body, parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            //..auto-set audit timestamps before save
            foreach (var entry in ChangeTracker.Entries<BaseEntity>()) {
                switch (entry.State) {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedOn = DateTime.Now;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
