using Argent.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Argent.Api.Infrastructure.Data {
    public class AppDataContext : DbContext {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

        //..application DnSets
        // public DbSet<Organization> Organizations => Set<Organization>();
        // public DbSet<Customer> Customers => Set<Customer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            //..apply all entity configurations from this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDataContext).Assembly);

            //..all entities with soft delete are filtered automatically
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .HasQueryFilter(
                            System.Linq.Expressions.Expression.Lambda(
                                System.Linq.Expressions.Expression.Equal(
                                    System.Linq.Expressions.Expression.Property(
                                        System.Linq.Expressions.Expression.Parameter(entityType.ClrType, "e"),
                                        "IsDeleted"),
                                    System.Linq.Expressions.Expression.Constant(false)),
                                System.Linq.Expressions.Expression.Parameter(entityType.ClrType, "e")));
                }
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            // Auto-set audit timestamps before save
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
