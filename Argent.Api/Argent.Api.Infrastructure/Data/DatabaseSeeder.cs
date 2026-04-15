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

            //..KYC General Customer
            ("CustomerKyc", "AllowManualRegistrationNumbers",  "false", ConfigDataType.Bool,   "Allow manual entry of client registration numbers"),
            ("CustomerKyc", "RegistrationNumberFormat","", ConfigDataType.String,"RegEx pattern for registration number format"),
            ("CustomerKyc", "RegistrationNumberLength","10",    ConfigDataType.Int,"Max length of registration numbers"),
            ("CustomerKyc", "RequireReferenceNumbers", "false", ConfigDataType.Bool,"Client reference number is required"),
            ("CustomerKyc", "AllowManualReferenceNumbers","false", ConfigDataType.Bool,"Allow manual entry of reference numbers"),
            ("CustomerKyc", "RequireStatisticNumbers", "false", ConfigDataType.Bool,"Client statistic number is required"),
            ("CustomerKyc", "RequirePermanentAddress","false", ConfigDataType.Bool,"Permanent address required"),
            ("CustomerKyc", "RequirePostalAddress","false", ConfigDataType.Bool,"Postal address required"),
            ("CustomerKyc", "RequireMobileNumber", "true",  ConfigDataType.Bool,"Mobile number required"),
            ("CustomerKyc", "RequireCustomerEmail","false", ConfigDataType.Bool,"Email address required"),
            ("CustomerKyc", "RequireVillageOfResidence","false", ConfigDataType.Bool,"Village of residence required"),
            ("CustomerKyc", "RequireDistrictOfResidence", "false", ConfigDataType.Bool,"District of residence required"),
            ("CustomerKyc", "RequireClientApproval","true",  ConfigDataType.Bool,"Require approval before activating customer"),
            ("CustomerKyc", "CanApproveOwnRegistrations", "false", ConfigDataType.Bool,"User can approve customers they registered"),
            ("CustomerKyc", "RequireRegistrationFees", "false", ConfigDataType.Bool,"Registration fees required at time of registration"),
            ("CustomerKyc", "CanTransactWithoutFees", "false", ConfigDataType.Bool,"Customer can transact before paying registration fees"),
            ("CustomerKyc", "SoftDeleteCustomerRecords","true",  ConfigDataType.Bool,"Soft-delete instead of permanently deleting customer records"),
            ("CustomerKyc", "Filter1Name", "Filter 1",ConfigDataType.String, "Label for shared client filter 1"),
            ("CustomerKyc", "Filter2Name", "Filter 2",ConfigDataType.String, "Label for shared client filter 2"),
            ("CustomerKyc", "Filter3Name", "Filter 3",ConfigDataType.String, "Label for shared client filter 3"),

            //..KYC Individual 
            ("CustomerKyc.Individual", "RequireMiddleName","false", ConfigDataType.Bool, "Middle name required"),
            ("CustomerKyc.Individual", "ClientMinimumAge","18",    ConfigDataType.Int,  "Minimum age to register as customer"),
            ("CustomerKyc.Individual", "RequireDateOfBirth", "false", ConfigDataType.Bool, "Date of birth required"),
            ("CustomerKyc.Individual", "RequirePhoto","false", ConfigDataType.Bool, "Customer photo required"),
            ("CustomerKyc.Individual", "RequireSignature","false", ConfigDataType.Bool, "Customer signature required"),
            ("CustomerKyc.Individual", "RequireNextOfKin","true",  ConfigDataType.Bool, "Next of kin contact required"),
            ("CustomerKyc.Individual", "RequireMaritalStatus","false", ConfigDataType.Bool, "Marital status required"),
            ("CustomerKyc.Individual", "RequireNationality","false", ConfigDataType.Bool, "Nationality required"),
            ("CustomerKyc.Individual", "RequireProfession","false", ConfigDataType.Bool, "Profession required"),
            ("CustomerKyc.Individual", "RequireEducation","false", ConfigDataType.Bool, "Education level required"),

            // KYC Group
            ("CustomerKyc.Group", "MaxGroupMembers", "30", ConfigDataType.Int,  "Maximum members in a single group"),
            ("CustomerKyc.Group", "TreatMembersAsIndividuals","false", ConfigDataType.Bool, "Treat group members as individual clients when transacting"),
            ("CustomerKyc.Group", "EnableClusters", "false", ConfigDataType.Bool, "Enable group sub-clusters"),
            ("CustomerKyc.Group", "ClustersAsGroups", "false", ConfigDataType.Bool, "Treat clusters as separate transacting groups"),
            ("CustomerKyc.Group", "MaxClusterMembers","10", ConfigDataType.Int,  "Maximum members per cluster"),
            ("CustomerKyc.Group", "GroupFilter1Name", "Group Filter 1", ConfigDataType.String, "Label for group filter 1"),
            ("CustomerKyc.Group", "GroupFilter2Name", "Group Filter 2", ConfigDataType.String, "Label for group filter 2"),
            ("CustomerKyc.Group", "MemberFilter1Name","Member Filter 1",ConfigDataType.String, "Label for member filter 1"),
            ("CustomerKyc.Group", "MemberFilter2Name","Member Filter 2",ConfigDataType.String, "Label for member filter 2"),

            //..KYC Business
            ("CustomerKyc.Business", "NumberOfSignatoriesRequired", "2",ConfigDataType.Int,"Minimum number of signatories required"),
            ("CustomerKyc.Business", "RequireSignatoryPhoto","false",ConfigDataType.Bool, "Signatory photo required"),
            ("CustomerKyc.Business", "RequireSignatoryIdentification","true",ConfigDataType.Bool, "Signatory identification document required"),
            ("CustomerKyc.Business", "RequireSignatorySignature", "false",ConfigDataType.Bool, "Signatory specimen signature required"),
            ("CustomerKyc.Business", "BusinessFilter1Name","Business Filter 1",ConfigDataType.String,"Label for business filter 1"),
            ("CustomerKyc.Business", "BusinessFilter2Name","Business Filter 2",ConfigDataType.String,"Label for business filter 2"),

            //..KYC File Storage
            ("CustomerKyc.Files", "MaxImageSizeKb", "2048",  ConfigDataType.Int, "Maximum image file size in KB"),
            ("CustomerKyc.Files", "MaxDocumentSizeKb", "5120",  ConfigDataType.Int,"Maximum document file size in KB"),
            ("CustomerKyc.Files", "AllowedImageTypes","jpg,png,jpeg", ConfigDataType.String, "Allowed image file extensions"),
            ("CustomerKyc.Files", "UseFtpStorage", "false", ConfigDataType.Bool,   "Use FTP for file storage instead of local"),
            ("CustomerKyc.Files", "ClientPhotoFolder", "photos",ConfigDataType.String, "Folder name for client photos"),
            ("CustomerKyc.Files", "ClientSignatureFolder","signatures",ConfigDataType.String,"Folder for client signatures"),
            ("CustomerKyc.Files", "IdentificationFolder", "ids",   ConfigDataType.String, "Folder for identification documents"),
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

            //..Customer KYC
            ("CustomerKyc.ViewIndividuals","CustomerKyc","ViewIndividuals","View individual customer records"),
            ("CustomerKyc.CreateIndividual","CustomerKyc", "CreateIndividual", "Register individual customers"),
            ("CustomerKyc.EditIndividual","CustomerKyc", "EditIndividual","Edit individual customer records"),
            ("CustomerKyc.ApproveIndividual","CustomerKyc", "ApproveIndividual","Approve individual customer registrations"),
            ("CustomerKyc.ExitIndividual","CustomerKyc", "ExitIndividual", "Mark individual customer as exited"),
            ("CustomerKyc.BlacklistCustomer","CustomerKyc", "BlacklistCustomer","Blacklist any customer type"),
            ("CustomerKyc.ViewGroups","CustomerKyc", "ViewGroups", "View group customer records"),
            ("CustomerKyc.CreateGroup","CustomerKyc", "CreateGroup", "Register group customers"),
            ("CustomerKyc.ManageGroupMembers", "CustomerKyc", "ManageGroupMembers", "Add/remove group members"),
            ("CustomerKyc.ViewBusinesses","CustomerKyc", "ViewBusinesses","View business customer records"),
            ("CustomerKyc.CreateBusiness","CustomerKyc", "CreateBusiness", "Register business customers"),
            ("CustomerKyc.ManageSignatories","CustomerKyc", "ManageSignatories","Add/edit business signatories"),
            ("CustomerKyc.ViewGuarantors","CustomerKyc", "ViewGuarantors","View guarantor records"),
            ("CustomerKyc.CreateGuarantor","CustomerKyc", "CreateGuarantor", "Register guarantors"),
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
