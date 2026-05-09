using Argent.Api.Domain.Entities.Access;
using Argent.Api.Domain.Entities.Settings;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Argent.Api.Infrastructure.Data {
    /// <summary>
    /// Idempotent startup seeder.
    /// Run order:
    ///   1. SystemConfigs
    ///   2. SystemPolicies
    ///   3. Permissions
    ///   4. Roles
    ///   5. RoleGroups
    ///   6. AdminPolicyOverrides
    ///   7. DefaultAdminUser
    ///   8. AccountingSeeder (currencies, COA, folios, journal/voucher types, document types)
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
                await SeedRoleGroupsAsync(context, logger);
                await SeedAdminPolicyOverridesAsync(context, logger);
                await SeedDefaultAdminUserAsync(context, logger);
                await AccountingSeeder.SeedAsync(context, logger);
                logger.Log("Database seeding complete.", "SEED");
            }
            catch (Exception ex) {
                logger.Log($"Seeding failed: {ex.Message}", "SEED-ERROR");
                logger.Log(ex.StackTrace ?? string.Empty, "STACKTRACE");
                throw;
            }
        }

        #region System Configurations

        private static readonly (string Module, string Key, string Value, ConfigDataType Type, string Description)[] Configs =
        [
            //..access and Password Policy
            ("Access", "PasswordMinLength","8",ConfigDataType.Int,  "Minimum password length"),
            ("Access", "PasswordRequireUppercase","true",ConfigDataType.Bool, "Require at least one uppercase letter"),
            ("Access", "PasswordRequireNumber","true",ConfigDataType.Bool, "Require at least one number"),
            ("Access", "PasswordRequireSpecialChar", "true",ConfigDataType.Bool, "Require at least one special character"),
            ("Access", "PasswordExpiryDays","90",ConfigDataType.Int,"Days before password expires (0 = never)"),
            ("Access", "AllowPasswordReuse","false",ConfigDataType.Bool,"Allow reuse of previous passwords"),
            ("Access", "PasswordHistoryCount","5",ConfigDataType.Int,"Number of previous passwords that cannot be reused"),
            ("Access", "MaxFailedLoginAttempts","5",ConfigDataType.Int,"Failed attempts before account lockout"),
            ("Access", "LockoutDurationMinutes","15",ConfigDataType.Int,"How long the account is locked after max failed attempts"),
            ("Access", "Require2FA","false", ConfigDataType.Bool, "Require two-factor authentication for all users"),
            ("Access", "SessionTimeoutMinutes","60",ConfigDataType.Int,"Idle session timeout in minutes"),
            ("Access", "AllowWeekendWork","false", ConfigDataType.Bool,"Allow users to log in and transact on weekends"),
            ("Access", "AllowHolidayWork","false", ConfigDataType.Bool,"Allow users to transact on branch holidays"),
            ("Access", "WorkdayStartHour","8",ConfigDataType.Int,"Start of working day (24h, EAT)"),
            ("Access", "WorkdayEndHour","17", ConfigDataType.Int,"End of working day (24h, EAT)"),
            ("Access", "EnforceWorkingHours", "false", ConfigDataType.Bool, "Block logins and transactions outside working hours"),

            //..kyc — General
            ("CustomerKyc", "AllowManualRegistrationNumbers", "false", ConfigDataType.Bool,"Allow manual entry of client registration numbers"),
            ("CustomerKyc", "RegistrationNumberLength","10",ConfigDataType.Int,"Max length of registration numbers"),
            ("CustomerKyc", "RequireReferenceNumbers", "false", ConfigDataType.Bool,"Client reference number is required"),
            ("CustomerKyc", "RequireMobileNumber","true",ConfigDataType.Bool,"Mobile number required"),
            ("CustomerKyc", "RequireCustomerEmail","false",ConfigDataType.Bool,"Email address required"),
            ("CustomerKyc", "RequireClientApproval","true",ConfigDataType.Bool,"Require approval before activating customer"),
            ("CustomerKyc", "CanApproveOwnRegistrations","false", ConfigDataType.Bool,"User can approve customers they registered"),
            ("CustomerKyc", "SoftDeleteCustomerRecords","true",ConfigDataType.Bool,"Soft-delete customer records"),

            //..kyc — Per-type filter labels
            ("CustomerKyc.Individual","Filter1Name","Individual Filter 1",ConfigDataType.String,"Label for individual customer filter slot 1"),
            ("CustomerKyc.Individual","Filter2Name","Individual Filter 2",ConfigDataType.String,"Label for individual customer filter slot 2"),
            ("CustomerKyc.Individual","Filter3Name","Individual Filter 3",ConfigDataType.String,"Label for individual customer filter slot 3"),
            ("CustomerKyc.Group","Filter1Name","Group Filter 1",ConfigDataType.String, "Label for group customer filter slot 1"),
            ("CustomerKyc.Group","Filter2Name","Group Filter 2",ConfigDataType.String, "Label for group customer filter slot 2"),
            ("CustomerKyc.Group","Filter3Name","Group Filter 3",ConfigDataType.String, "Label for group customer filter slot 3"),
            ("CustomerKyc.Group","GroupFilter1Name","Group Class 1",ConfigDataType.String, "Label for group-specific filter slot 1"),
            ("CustomerKyc.Group","GroupFilter2Name","Group Class 2",ConfigDataType.String, "Label for group-specific filter slot 2"),
            ("CustomerKyc.Member","Filter1Name","Member Filter 1",ConfigDataType.String,"Label for group member filter slot 1"),
            ("CustomerKyc.Member","Filter2Name","Member Filter 2",ConfigDataType.String,"Label for group member filter slot 2"),
            ("CustomerKyc.Member","Filter3Name","Member Filter 3",ConfigDataType.String,"Label for group member filter slot 3"),
            ("CustomerKyc.Member","MemberFilter1Name","Member Class 1", ConfigDataType.String,"Label for member-specific filter slot 1"),
            ("CustomerKyc.Member","MemberFilter2Name","Member Class 2", ConfigDataType.String,"Label for member-specific filter slot 2"),
            ("CustomerKyc.Business","Filter1Name","Business Filter 1",ConfigDataType.String,"Label for business customer filter slot 1"),
            ("CustomerKyc.Business","Filter2Name","Business Filter 2",ConfigDataType.String,"Label for business customer filter slot 2"),
            ("CustomerKyc.Business","Filter3Name","Business Filter 3",ConfigDataType.String,"Label for business customer filter slot 3"),
            ("CustomerKyc.Business","BusinessFilter1Name", "Business Class 1",ConfigDataType.String,"Label for business-specific filter slot 1"),
            ("CustomerKyc.Business","BusinessFilter2Name", "Business Class 2",ConfigDataType.String,"Label for business-specific filter slot 2"),

            //..kyc — Individual
            ("CustomerKyc.Individual","ClientMinimumAge","18",ConfigDataType.Int,"Minimum age to register as customer"),
            ("CustomerKyc.Individual","RequireDateOfBirth","false", ConfigDataType.Bool, "Date of birth required"),
            ("CustomerKyc.Individual","RequirePhoto","false", ConfigDataType.Bool,"Customer photo required"),
            ("CustomerKyc.Individual","RequireNextOfKin","true",ConfigDataType.Bool,"Next of kin contact required"),
            ("CustomerKyc.Individual","RequireNationality","false", ConfigDataType.Bool, "Nationality required"),

            //..kyc — Group
            ("CustomerKyc.Group","MaxGroupMembers","30",ConfigDataType.Int,"Maximum members in a single group"),
            ("CustomerKyc.Group","EnableClusters","false",ConfigDataType.Bool,"Enable group sub-clusters"),
            ("CustomerKyc.Group","MaxClusterMembers","10",ConfigDataType.Int,"Maximum members per cluster"),

            //..kyc — Business 
            ("CustomerKyc.Business","NumberOfSignatoriesRequired","2",ConfigDataType.Int,"Minimum signatories required"),
            ("CustomerKyc.Business","RequireSignatoryIdentification","true",ConfigDataType.Bool,"Signatory identification document required"),
        ];

        private static async Task SeedSystemConfigsAsync(AppDataContext context, IServiceLogger logger) {
            var existing = await context.SystemConfigs
                .Select(c => c.Module + "||" + c.Key)
                .ToHashSetAsync();

            var toInsert = Configs
                .Where(c => !existing.Contains(c.Module + "||" + c.Key))
                .Select(c => new SystemConfiguration {
                    Module = c.Module,
                    Key = c.Key,
                    Value = c.Value,
                    DataType = c.Type,
                    Description = c.Description,
                    IsEditable = true,
                    CreatedBy = "SYSTEM"
                }).ToList();

            if (toInsert.Count > 0) {
                await context.SystemConfigs.AddRangeAsync(toInsert);
                await context.SaveChangesAsync();
                logger.Log($"Seeded {toInsert.Count} system config(s).", "SEED");
            }
            else
                logger.Log("System configs already seeded.", "SEED");
        }

        #endregion

        #region System Policies

        private static readonly (string Name, string Module, string Description, string Default, ConfigDataType Type, bool Overridable)[] Policies =
        [
            ("AllowWeekendWork","Access", "Allow transactions on Saturdays and Sundays", "false", ConfigDataType.Bool, true),
            ("AllowHolidayWork","Access", "Allow transactions on branch holidays","false",ConfigDataType.Bool, true),
            ("EnforceWorkingHours","Access", "Restrict logins to configured working hours","false",ConfigDataType.Bool, true),
            ("Require2FA","Access","Require 2FA for login","false", ConfigDataType.Bool, false),
            ("AllowPasswordReuse","Access", "Allow users to reuse previous passwords","false", ConfigDataType.Bool, false),
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
            else
                logger.Log("System policies already seeded.", "SEED");
        }

        #endregion

        #region Permissions

        private static readonly (string Name, string Module, string Action, string Description)[] AllPermissions =
        [
            ("Organization.View","Organization", "View","View organization profile and branches"),
            ("Organization.Edit","Organization", "Edit","Edit organization profile"),
            ("Organization.ManageBranches","Organization", "ManageBranches","Create and configure branches"),
            ("Organization.ManageHolidays","Organization", "ManageHolidays","Add and remove branch holidays"),
            ("Access.ViewUsers","Access", "ViewUsers","View system users"),
            ("Access.CreateUser", "Access", "CreateUser","Create new system users"),
            ("Access.EditUser","Access", "EditUser","Edit user profile and status"),
            ("Access.ManageRoles","Access", "ManageRoles","Create and assign roles"),
            ("Access.ManageRoleGroups","Access", "ManageRoleGroups","Create and manage role groups"),
            ("Access.ManageBranchAccess","Access", "ManageBranchAccess","Grant/revoke branch access"),
            ("Access.ManagePolicies","Access", "ManagePolicies","Edit system policies and overrides"),
            ("SystemConfig.View","SystemConfig", "View","View system configuration"),
            ("SystemConfig.Edit","SystemConfig", "Edit","Edit system configuration values"),
            ("Audits.View","Audits", "View","View system audit logs"),
            ("Audits.Export","Audits", "Export","Export audit log data"),
            ("CustomerKyc.ViewIndividuals","CustomerKyc", "ViewIndividuals","View individual customer records"),
            ("CustomerKyc.CreateIndividual","CustomerKyc","CreateIndividual","Register individual customers"),
            ("CustomerKyc.EditIndividual","CustomerKyc","EditIndividual","Edit individual customer records"),
            ("CustomerKyc.ApproveIndividual","CustomerKyc","ApproveIndividual","Approve individual customer registrations"),
            ("CustomerKyc.ExitIndividual","CustomerKyc","ExitIndividual","Mark individual customer as exited"),
            ("CustomerKyc.BlacklistCustomer","CustomerKyc","BlacklistCustomer", "Blacklist any customer type"),
            ("CustomerKyc.ViewGroups","CustomerKyc","ViewGroups","View group customer records"),
            ("CustomerKyc.CreateGroup","CustomerKyc","CreateGroup","Register group customers"),
            ("CustomerKyc.ManageGroupMembers","CustomerKyc","ManageGroupMembers","Add/remove group members"),
            ("CustomerKyc.ViewBusinesses","CustomerKyc","ViewBusinesses","View business customer records"),
            ("CustomerKyc.CreateBusiness","CustomerKyc","CreateBusiness","Register business customers"),
            ("CustomerKyc.ManageSignatories","CustomerKyc","ManageSignatories","Add/edit business signatories"),
            ("CustomerKyc.ViewGuarantors","CustomerKyc","ViewGuarantors","View guarantor records"),
            ("CustomerKyc.CreateGuarantor","CustomerKyc","CreateGuarantor","Register guarantors"),
            ("Accounting.ViewChartOfAccounts","Accounting", "ViewChartOfAccounts","View chart of accounts"),
            ("Accounting.ManageChartOfAccounts","Accounting","ManageChartOfAccounts","Create and edit ledger accounts"),
            ("Accounting.ViewLedger","Accounting", "ViewLedger","View general ledger"),
            ("Accounting.PostJournals","Accounting", "PostJournals","Create and post journal entries"),
            ("Accounting.ApproveJournals","Accounting", "ApproveJournals","Approve journal entries"),
            ("Accounting.ManageVouchers","Accounting", "ManageVouchers","Create and manage vouchers"),
            ("Accounting.ManageBankAccounts","Accounting", "ManageBankAccounts","Manage bank accounts"),
            ("Accounting.BankReconciliation", "Accounting", "BankReconciliation","Perform bank reconciliation"),
            ("Accounting.ManageCashiers","Accounting", "ManageCashiers","Manage cashier accounts"),
            ("Accounting.FinancialYear","Accounting", "FinancialYear","Manage financial years and period closures"),
            ("Accounting.ViewReports","Accounting", "ViewReports","View accounting reports"),
        ];

        private static async Task SeedPermissionsAsync(AppDataContext context, IServiceLogger logger) {
            var existing = await context.Permissions.Select(p => p.Name).ToHashSetAsync();
            var toInsert = AllPermissions
                .Where(p => !existing.Contains(p.Name))
                .Select(p => new Permission
                {
                    Name = p.Name,
                    Module = p.Module,
                    Action = p.Action,
                    Description = p.Description,
                    CreatedBy = "SYSTEM"
                }).ToList();

            if (toInsert.Count > 0) {
                await context.Permissions.AddRangeAsync(toInsert);
                await context.SaveChangesAsync();
                logger.Log($"Seeded {toInsert.Count} permission(s).", "SEED");
            }
            else
                logger.Log("Permissions already seeded.", "SEED");
        }

        #endregion

        #region Roles

        private static async Task SeedRolesAsync(AppDataContext context, IServiceLogger logger) {
            // null = all permissions
            await EnsureRoleAsync(context, logger, "System Administrator", "Full system access.", true, permissionNames: null);  

            await EnsureRoleAsync(context, logger,
                "Branch Manager", "Full operational access within assigned branches.", true,
                permissionNames:
                [
                    "Organization.View",
                    "Organization.ManageHolidays",
                    "Access.ViewUsers",
                    "Audits.View",
                    "CustomerKyc.ViewIndividuals", 
                    "CustomerKyc.CreateIndividual",
                    "CustomerKyc.ApproveIndividual",
                    "CustomerKyc.ExitIndividual",
                    "CustomerKyc.BlacklistCustomer",
                    "CustomerKyc.ViewGroups",
                    "CustomerKyc.CreateGroup",
                    "CustomerKyc.ManageGroupMembers",
                    "CustomerKyc.ViewBusinesses",
                    "CustomerKyc.CreateBusiness",
                    "Accounting.ViewLedger",
                    "Accounting.ViewReports",
                    "Accounting.ManageVouchers",
                    "Accounting.BankReconciliation"
                ]);

            await EnsureRoleAsync(context, logger, "Accountant", "Full accounting access for journals, vouchers, reconciliation.", true,
                permissionNames:
                [
                    "Organization.View",
                    "Accounting.ViewChartOfAccounts",
                    "Accounting.ManageChartOfAccounts",
                    "Accounting.ViewLedger",
                    "Accounting.PostJournals",
                    "Accounting.ApproveJournals", 
                    "Accounting.ManageVouchers",
                    "Accounting.ManageBankAccounts", 
                    "Accounting.BankReconciliation",
                    "Accounting.ManageCashiers", 
                    "Accounting.FinancialYear",
                    "Accounting.ViewReports"
                ]);

            await EnsureRoleAsync(context, logger, "Teller Supervisor", "Oversees teller transactions and approvals.", true,
                permissionNames:
                [
                    "Organization.View",
                    "CustomerKyc.ViewIndividuals", 
                    "CustomerKyc.ViewGroups",
                    "CustomerKyc.ViewBusinesses",
                    "Accounting.ViewLedger", 
                    "Accounting.ManageVouchers",
                    "Accounting.ViewReports"
                ]);

            await EnsureRoleAsync(context, logger, "Teller", "Posts day-to-day transactions.", true,
                permissionNames:
                [
                    "Organization.View",
                    "CustomerKyc.ViewIndividuals", 
                    "CustomerKyc.ViewGroups",
                    "CustomerKyc.ViewBusinesses",
                    "Accounting.ManageVouchers"
                ]);
        }

        private static async Task EnsureRoleAsync(
            AppDataContext context, IServiceLogger logger,
            string name, string description, bool isSystemRole,
            string[]? permissionNames) {
            if (await context.Roles.AnyAsync(r => r.Name == name)) return;

            var role = new Role
            {
                Name = name,
                Description = description,
                IsSystemRole = isSystemRole,
                CreatedBy = "SYSTEM"
            };
            await context.Roles.AddAsync(role);
            await context.SaveChangesAsync();

            var perms = permissionNames is null
                ? await context.Permissions.ToListAsync()
                : await context.Permissions
                    .Where(p => permissionNames.Contains(p.Name))
                    .ToListAsync();

            await context.RolePermissions.AddRangeAsync(
                perms.Select(p => new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = p.Id,
                    CreatedBy = "SYSTEM"
                }));
            await context.SaveChangesAsync();
            logger.Log($"Role '{name}' seeded with {perms.Count} permission(s).", "SEED");
        }

        #endregion

        #region Role Groups

        private static async Task SeedRoleGroupsAsync(AppDataContext context, IServiceLogger logger) {
            await EnsureRoleGroupAsync(context, logger, "Administrators", "System and branch administrators.",
                ["System Administrator", "Branch Manager"]);

            await EnsureRoleGroupAsync(context, logger, "Accountants", "Finance and accounting team.",
                ["Accountant"]);

            await EnsureRoleGroupAsync(context, logger, "Tellers", "Counter staff.",
                ["Teller Supervisor", "Teller"]);
        }

        private static async Task EnsureRoleGroupAsync(
            AppDataContext context, IServiceLogger logger,
            string name, string description, string[] roleNames) {
            if (await context.RoleGroups.AnyAsync(g => g.Name == name)) return;

            var group = new RoleGroup
            {
                Name = name,
                Description = description,
                IsActive = true,
                CreatedBy = "SYSTEM"
            };
            await context.RoleGroups.AddAsync(group);
            await context.SaveChangesAsync();

            var roles = await context.Roles
                .Where(r => roleNames.Contains(r.Name))
                .ToListAsync();

            await context.RoleGroupMembers.AddRangeAsync(
                roles.Select(r => new RoleGroupMember
                {
                    RoleGroupId = group.Id,
                    RoleId = r.Id,
                    CreatedBy = "SYSTEM"
                }));
            await context.SaveChangesAsync();
            logger.Log($"RoleGroup '{name}' seeded with {roles.Count} role(s).", "SEED");
        }

        #endregion

        #region Admin Policy Overrides

        private static async Task SeedAdminPolicyOverridesAsync(
            AppDataContext context, IServiceLogger logger) {
            var adminGroup = await context.RoleGroups
                .FirstOrDefaultAsync(g => g.Name == "Administrators");
            if (adminGroup is null) {
                logger.Log("Administrators group not found — skipping policy override seeding.", "SEED");
                return;
            }

            var overrides = new[]
            {
                ("AllowWeekendWork","true","Administrators can work on weekends"),
                ("AllowHolidayWork","true","Administrators can work on branch holidays"),
                ("EnforceWorkingHours","false","Administrators are not restricted to working hours"),
            };

            var seededCount = 0;
            foreach (var (policyName, value, reason) in overrides) {
                var policy = await context.SystemPolicies
                    .FirstOrDefaultAsync(p => p.Name == policyName);
                if (policy is null) continue;

                var exists = await context.RoleGroupPolicyOverrides
                    .AnyAsync(o => o.RoleGroupId == adminGroup.Id && o.SystemPolicyId == policy.Id);
                if (exists) continue;

                await context.RoleGroupPolicyOverrides.AddAsync(new RoleGroupPolicyOverride
                {
                    RoleGroupId = adminGroup.Id,
                    SystemPolicyId = policy.Id,
                    OverrideValue = value,
                    Reason = reason,
                    CreatedBy = "SYSTEM"
                });
                seededCount++;
            }

            if (seededCount > 0) {
                await context.SaveChangesAsync();
                logger.Log($"Seeded {seededCount} admin policy override(s).", "SEED");
            }
            else
                logger.Log("Admin policy overrides already seeded.", "SEED");
        }

        #endregion

        #region Default Admin User

        /// <summary>
        /// Seeds the initial System Administrator user.
        ///
        /// This user requires a branch to exist before it can be created.
        /// The seeder checks for an existing branch — if none exists, it skips
        /// user seeding and logs a warning. The admin user should be created via
        /// the setup wizard / first-run API after the organization and branch are set up.
        ///
        /// Default credentials (CHANGE IMMEDIATELY after first login):
        ///   Username: admin
        ///   Password: Admin@1234!
        /// </summary>
        private static async Task SeedDefaultAdminUserAsync(
            AppDataContext context, IServiceLogger logger) {
            if (await context.Users.AnyAsync(u => u.Username == "admin")) {
                logger.Log("Admin user already exists.", "SEED");
                return;
            }

            // Admin user requires a branch — skip if none has been created yet
            var defaultBranch = await context.Branches.FirstOrDefaultAsync(b => b.IsDefault && !b.IsDeleted);

            if (defaultBranch is null) {
                logger.Log("No default branch found, admin user will be created after first-run setup.", "SEED-WARN");
                return;
            }

            var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "System Administrator");
            if (adminRole is null) {
                logger.Log("System Administrator role not found, we are skipping admin user seed.", "SEED-WARN");
                return;
            }

            var admin = new AppUser
            {
                Username = "admin",
                Email = "admin@mail.com",
                FirstName = "System",
                MiddleName = "",
                LastName = "Admin",
                PhoneNumber = "256700000000",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password@10"),
                DefaultBranchId = defaultBranch.Id,
                IsActive = true,
                FailedLoginAttempts = 0,
                CreatedBy = "SYSTEM"
            };

            await context.Users.AddAsync(admin);
            await context.SaveChangesAsync();

            await context.UserRoles.AddAsync(new UserRole
            {
                UserId = admin.Id,
                RoleId = adminRole.Id,
                CreatedBy = "SYSTEM"
            });

            await context.UserBranchAccess.AddAsync(new UserBranchAccess
            {
                UserId = admin.Id,
                BranchId = defaultBranch.Id,
                CanPost = true,
                CreatedBy = "SYSTEM"
            });

            await context.SaveChangesAsync();
            logger.Log("Default admin user seeded. Username: admin, CHANGE PASSWORD IMMEDIATELY.", "SEED-WARN");
        }

        #endregion
    }

}
