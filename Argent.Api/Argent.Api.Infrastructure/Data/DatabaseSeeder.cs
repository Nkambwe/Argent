using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Argent.Api.Infrastructure.Data {
    /// <summary>
    /// Seeds the database with system permissions and default roles on first startup.
    /// Safe to call on every startup — checks existence before inserting.
    /// Add new permissions here as each module is built.
    /// </summary>
    public static class DatabaseSeeder {
        public static async Task SeedAsync(IServiceProvider services) {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDataContext>();
            var loggerFactory = scope.ServiceProvider.GetRequiredService<IServiceLoggerFactory>();
            var logger = loggerFactory.CreateLogger("startup");
            logger.Channel = "DB-SEED";

            try {
                logger.Log("Running database seeder...", "SEED");
                await SeedPermissionsAsync(context, logger);
                await SeedRolesAsync(context, logger);
                logger.Log("Database seeding complete.", "SEED");
            }
            catch (Exception ex) {
                logger.Log($"Seeding failed: {ex.Message}", "SEED-ERROR");
                logger.Log(ex.StackTrace ?? string.Empty, "STACKTRACE");
                throw;
            }
        }

        //..permission definitions
        // Format: (Name, Module, Action, Description)
        // Add new module permissions here as each module is built.

        private static readonly (string Name, string Module, string Action, string Description)[] AllPermissions =
        [
            //..organization
            ("Organization.View","Organization", "View","View organization profile and branches"),
            ("Organization.Edit","Organization", "Edit", "Edit organization profile"),
            ("Organization.ManageBranches","Organization", "ManageBranches","Create and configure branches"),

            //..system Access
            ("Access.ViewUsers", "Access","ViewUsers",  "View system users"),
            ("Access.CreateUser","Access","CreateUser","Create new system users"),
            ("Access.EditUser", "Access","EditUser", "Edit user profile and status"),
            ("Access.ManageRoles","Access", "ManageRoles",  "Create and assign roles"),
            ("Access.ManageBranchAccess", "Access", "ManageBranchAccess", "Grant/revoke branch access for users"),

            //..audits
            ("Audits.View", "Audits","View", "View system audit logs"),
            ("Audits.Export", "Audits", "Export","Export audit log data"),

            // Customer KYC (placeholder — uncomment when module is built)
            // ("CustomerKyc.Create",     "CustomerKyc",  "Create",       "Register new customers"),
            // ("CustomerKyc.View",       "CustomerKyc",  "View",         "View customer records"),
            // ("CustomerKyc.Edit",       "CustomerKyc",  "Edit",         "Edit customer information"),
            // ("CustomerKyc.Approve",    "CustomerKyc",  "Approve",      "Approve customer KYC"),

            // Savings (placeholder)
            // ("Savings.Create",         "Savings",      "Create",       "Open savings accounts"),
            // ("Savings.Deposit",        "Savings",      "Deposit",      "Post deposits"),
            // ("Savings.Withdraw",       "Savings",      "Withdraw",     "Post withdrawals"),
            // ("Savings.Approve",        "Savings",      "Approve",      "Approve savings transactions"),
        ];

        private static async Task SeedPermissionsAsync(AppDataContext context, IServiceLogger logger) {
            var existingNames = await context.Permissions.Select(p => p.Name).ToHashSetAsync();
            var toInsert = AllPermissions
                .Where(p => !existingNames.Contains(p.Name))
                .Select(p => new Permission
                {
                    Name = p.Name,
                    Module = p.Module,
                    Action = p.Action,
                    Description = p.Description,
                    CreatedBy = "system"
                })
                .ToList();

            if (toInsert.Count > 0) {
                await context.Permissions.AddRangeAsync(toInsert);
                await context.SaveChangesAsync();
                logger.Log($"Seeded {toInsert.Count} permission(s).", "SEED");
            }
            else {
                logger.Log("Permissions already seeded — skipping.", "SEED");
            }
        }

        private static async Task SeedRolesAsync(AppDataContext context, IServiceLogger logger) {
            await EnsureRoleAsync(context, logger,
                new RoleGroup() {
                    Name = "System Administrators",
                    Description = "System Administrator Related Roles",
                    IsSystemGroup = true,
                    CreatedBy = "SYSTEM"
                },
                name: "System Administrator",
                description: "Full access to all modules and settings.",
                isSystemRole: true,
                moduleFilter: null); 
            await EnsureRoleAsync(context, logger,
                new RoleGroup()
                {
                    Name = "Branch Managers",
                    Description = "Branch Manager Related Roles",
                    IsSystemGroup = true,
                    CreatedBy = "SYSTEM"
                },
                name: "Branch Manager",
                description: "Full operational access within assigned branches.",
                isSystemRole: true,
                moduleFilter: ["Organization", "Audits"]);
            await EnsureRoleAsync(context, logger,
                new RoleGroup()
                {
                    Name = "Tellers",
                    Description = "Teller Related Roles",
                    IsSystemGroup = true,
                    CreatedBy = "SYSTEM"
                },
                name: "Teller",
                description: "Posts day-to-day branch transactions.",
                isSystemRole: true,
                moduleFilter: null,
                permissionFilter: p => p.Action is "Deposit" or "Withdraw" or "View");
        }

        private static async Task EnsureRoleAsync(AppDataContext context, IServiceLogger logger, RoleGroup group, string name, string description, bool isSystemRole, string[]? moduleFilter, Func<Permission, bool>? permissionFilter = null) {
            var exists = await context.Roles.AnyAsync(r => r.Name == name);
            if (exists) {
                logger.Log($"Role '{name}' already exists — skipping.", "SEED");
                return;
            }

            // Ensure role group exists first
            var existingGroup = await context.RoleGroups
                .FirstOrDefaultAsync(g => g.Name == group.Name);

            if (existingGroup is null) {
                await context.RoleGroups.AddAsync(group);
                await context.SaveChangesAsync();
                existingGroup = group;
            }

            var role = new Role {
                Name = name,
                Description = description,
                IsSystemRole = isSystemRole,
                RoleGroupId = existingGroup.Id, 
                CreatedBy = "SYSTEM"
            };

            await context.Roles.AddAsync(role);
            await context.SaveChangesAsync();

            var query = context.Permissions.AsQueryable();

            if (moduleFilter is not null)
                query = query.Where(p => moduleFilter.Contains(p.Module));

            var permissions = await query.ToListAsync();

            if (permissionFilter is not null)
                permissions = permissions.Where(permissionFilter).ToList();

            var rolePermissions = permissions.Select(p => new RolePermission
            {
                RoleId = role.Id,
                PermissionId = p.Id,
                CreatedBy = "SYSTEM"
            });

            await context.RolePermissions.AddRangeAsync(rolePermissions);
            await context.SaveChangesAsync();

            logger.Log($"Role '{name}' seeded with {permissions.Count} permission(s).", "SEED");
        }
    }

}
