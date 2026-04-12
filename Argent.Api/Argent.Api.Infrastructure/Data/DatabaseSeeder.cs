using Argent.Api.Domain.Entities.Access;
using Argent.Api.Domain.Entities.Settings;
using Argent.Api.Domain.Enums;
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
                await SeedSystemConfigsAsync(context, logger);
                await SeedSystemPoliciesAsync(context, logger);
                await SeedPermissionsAsync(context, logger);
                await SeedRolesAsync(context, logger);
                logger.Log("Database seeding complete.", "SEED");
            } catch (Exception ex) {
                logger.Log($"Seeding failed: {ex.Message}", "SEED-ERROR");
                logger.Log(ex.StackTrace ?? string.Empty, "STACKTRACE");
                throw;
            }
        }

        /// <summary>
        /// System access and password, and work policies
        /// </summary>
        private static readonly (string Module, string Key, string Value, ConfigDataType Type, string Description)[] Configs =
        [
            //..access and Password Policy
            ("Access", "PasswordMinLength","8",ConfigDataType.Int,"Minimum password length"),
            ("Access", "PasswordRequireUppercase","true", ConfigDataType.Bool, "Require at least one uppercase letter"),
            ("Access", "PasswordRequireNumber","true", ConfigDataType.Bool, "Require at least one number"),
            ("Access", "PasswordRequireSpecialChar", "true", ConfigDataType.Bool, "Require at least one special character"),
            ("Access", "PasswordExpiryDays","90",ConfigDataType.Int,"Days before password expires (0 = never)"),
            ("Access", "AllowPasswordReuse", "false",ConfigDataType.Bool, "Allow reuse of previous passwords"),
            ("Access", "PasswordHistoryCount","5",ConfigDataType.Int,"Number of previous passwords that cannot be reused"),
            ("Access", "MaxFailedLoginAttempts","5",    ConfigDataType.Int,"Failed attempts before account lockout"),
            ("Access", "LockoutDurationMinutes","15",   ConfigDataType.Int,"How long the account is locked after max failed attempts"),
            ("Access", "Require2FA","false",ConfigDataType.Bool, "Require two-factor authentication for all users"),
            ("Access", "SessionTimeoutMinutes","60",   ConfigDataType.Int,"Idle session timeout in minutes"),

            //..access and Working Hours Policy
            ("Access", "AllowWeekendWork","false",ConfigDataType.Bool, "Allow users to log in and transact on weekends"),
            ("Access", "AllowHolidayWork","false",ConfigDataType.Bool, "Allow users to transact on branch holidays"),
            ("Access", "WorkdayStartHour","8",ConfigDataType.Int,  "Start of working day (24h, EAT)"),
            ("Access", "WorkdayEndHour","17",ConfigDataType.Int,  "End of working day (24h, EAT)"),
            ("Access", "EnforceWorkingHours","false",ConfigDataType.Bool, "Block logins and transactions outside working hours"),
        ];

        /// <summary>
        /// Seed system configurations
        /// </summary>
        /// <param name="context">DB context instance</param>
        /// <param name="logger">System Logger instance</param>
        /// <returns></returns>
        private static async Task SeedSystemConfigsAsync(AppDataContext context, IServiceLogger logger) {
            var existing = await context.SystemConfigs
                .Select(c => c.Module + "." + c.Key)
                .ToHashSetAsync();

            var toInsert = Configs
                .Where(c => !existing.Contains(c.Module + "." + c.Key))
                .Select(c => new SystemConfiguration
                {
                    Module = c.Module,
                    Key = c.Key,
                    Value = c.Value,
                    DataType = c.Type,
                    Description = c.Description,
                    IsEditable = true,
                    CreatedBy = "system"
                }).ToList();

            if (toInsert.Count > 0) {
                await context.SystemConfigs.AddRangeAsync(toInsert);
                await context.SaveChangesAsync();
                logger.Log($"Seeded {toInsert.Count} system config(s).", "SEED");
            }
            else logger.Log("System configs already seeded.", "SEED");
        }

        /// <summary>
        /// System configurations
        /// </summary>
        private static readonly (string Name, string Module, string Description, string Default, ConfigDataType Type, bool Overridable)[] Policies =
        [
            ("AllowWeekendWork",  "Access", "Allow transactions on Saturdays and Sundays", "false", ConfigDataType.Bool, true),
            ("AllowHolidayWork",  "Access", "Allow transactions on branch holidays","false", ConfigDataType.Bool, true),
            ("EnforceWorkingHours","Access","Restrict logins to configured working hours","false", ConfigDataType.Bool, true),
            ("Require2FA","Access", "Require 2FA for login","false", ConfigDataType.Bool, false),
        ];

        private static async Task SeedSystemPoliciesAsync(AppDataContext context, IServiceLogger logger) {
            var existing = await context.SystemPolicies
                .Select(p => p.Name)
                .ToHashSetAsync();

            var toInsert = Policies
                .Where(p => !existing.Contains(p.Name))
                .Select(p => new SystemPolicy
                {
                    Name = p.Name,
                    Module = p.Module,
                    Description = p.Description,
                    DefaultValue = p.Default,
                    DataType = p.Type,
                    IsOverridable = p.Overridable,
                    CreatedBy = "SYSTEM"
                }).ToList();

            if (toInsert.Count > 0) {
                await context.SystemPolicies.AddRangeAsync(toInsert);
                await context.SaveChangesAsync();
                logger.Log($"Seeded {toInsert.Count} system polic(ies).", "SEED");
            }
            else logger.Log("System policies already seeded.", "SEED");
        }

        /// <summary>
        ///..permission definitions
        /// </summary>
        /// <remarks>
        /// Format: (Name, Module, Action, Description).Add new module permissions here as each module is built. 
        /// </remarks>
        private static readonly (string Name, string Module, string Action, string Description)[] AllPermissions =
        [
            //..organization
            ("Organization.View","Organization", "View","View organization profile and branches"),
            ("Organization.Edit","Organization", "Edit", "Edit organization profile"),
            ("Organization.ManageBranches","Organization", "ManageBranches","Create and configure branches"),

            //..system Access
            ("Access.ViewUsers","Access","ViewUsers","View system users"),
            ("Access.CreateUser","Access","CreateUser","Create new system users"),
            ("Access.EditUser","Access","EditUser","Edit user profile and status"),
            ("Access.ManageRoles","Access","ManageRoles","Create and assign roles"),
            ("Access.ManageRoleGroups","Access", "ManageRoleGroups","Create and manage role groups"),
            ("Access.ManageBranchAccess","Access","ManageBranchAccess","Grant or revoke branch access"),
            ("Access.ManagePolicies","Access","ManagePolicies","Edit system policies and overrides"),
            ("SystemConfig.View","SystemConfig","View","View system configuration"),
            ("SystemConfig.Edit","SystemConfig","Edit","Edit system configuration values"),

            //..audits
            ("Audits.View", "Audits","View", "View system audit logs"),
            ("Audits.Export", "Audits", "Export","Export audit log data"),
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
                    CreatedBy = "SYSTEM"
                })
                .ToList();

            if (toInsert.Count > 0) {
                await context.Permissions.AddRangeAsync(toInsert);
                await context.SaveChangesAsync();
                logger.Log($"Seeded {toInsert.Count} permission(s).", "SEED");
            } else {
                logger.Log("Permissions already seeded — skipping.", "SEED");
            }
        }

        private static async Task SeedRolesAsync(AppDataContext context, IServiceLogger logger) {
            //..add roles
            //..null = all permissions
            await EnsureRoleAsync(context, logger, "System Administrator", "Full system access.", true, permissionNames: null); 
            
            await EnsureRoleAsync(context, logger, "Branch Manager","Full operational access within assigned branches.", true,
                permissionNames: ["Organization.View", "Organization.ManageHolidays", "Access.ViewUsers",  "Audits.View"]);
            
            await EnsureRoleAsync(context, logger, "Teller Supervisor","Oversees teller transactions and approvals.", true,
                permissionNames: ["Organization.View"]);
            
            await EnsureRoleAsync(context, logger, "Teller", "Posts day-to-day transactions.", true,
                permissionNames: ["Organization.View"]);

            //..ad roleGroups
            await EnsureRoleGroupAsync(context, logger, "Administrators", "System and branch administrators.", 
                ["System Administrator", "Branch Manager"]);

            await EnsureRoleGroupAsync(context, logger, "Tellers",
                "Counter staff.", ["Teller Supervisor", "Teller"]);
        }

        private static async Task EnsureRoleAsync(AppDataContext context, IServiceLogger logger, string name, string description, bool isSystemRole, string[]? permissionNames) {
            if (await context.Roles.AnyAsync(r => r.Name == name)) return;

            var role = new Role {
                Name = name,
                Description = description,
                IsSystemRole = isSystemRole,
                CreatedBy = "SYSTEM"
            };
            await context.Roles.AddAsync(role);
            await context.SaveChangesAsync();

            var perms = permissionNames is null ? await context.Permissions.ToListAsync() :
                        await context.Permissions.Where(p => permissionNames.Contains(p.Name)).ToListAsync();

            await context.RolePermissions.AddRangeAsync(perms.Select(p => new RolePermission
                { 
                    RoleId = role.Id,
                    PermissionId = p.Id,
                    CreatedBy = "SYSTEM"
                }
            ));

            await context.SaveChangesAsync();
            logger.Log($"Role '{name}' seeded with {perms.Count} permission(s).", "SEED");
        }

        private static async Task EnsureRoleGroupAsync(AppDataContext context, IServiceLogger logger,
            string name, string description, string[] roleNames) {
            if (await context.RoleGroups.AnyAsync(g => g.Name == name)) return;

            var group = new RoleGroup { Name = name, Description = description, CreatedBy = "system" };
            await context.RoleGroups.AddAsync(group);
            await context.SaveChangesAsync();

            var roles = await context.Roles.Where(r => roleNames.Contains(r.Name)).ToListAsync();
            await context.RoleGroupMembers.AddRangeAsync(roles.Select(r => new RoleGroupMember
                { 
                    RoleGroupId = group.Id,
                    RoleId = r.Id,
                    CreatedBy = "SYSTEM"
                }
            ));

            await context.SaveChangesAsync();
            logger.Log($"RoleGroup '{name}' seeded with {roles.Count} role(s).", "SEED");
        }
    }

}
