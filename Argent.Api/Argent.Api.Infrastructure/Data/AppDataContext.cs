using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities;
using Argent.Api.Domain.Entities.Access;
using Argent.Api.Domain.Entities.Audit;
using Argent.Api.Domain.Entities.Banking.Savings;
using Argent.Api.Domain.Entities.Kyc;
using Argent.Api.Domain.Entities.Kyc.KycBusinesses;
using Argent.Api.Domain.Entities.Kyc.KycFiles;
using Argent.Api.Domain.Entities.Kyc.KycGroups;
using Argent.Api.Domain.Entities.Kyc.KycIndividuals;
using Argent.Api.Domain.Entities.Settings;
using Argent.Api.Domain.Entities.Support.KycLookup;
using Argent.Api.Domain.Entities.Support.KycSupport;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Argent.Api.Infrastructure.Data {
    public class AppDataContext(DbContextOptions<AppDataContext> options, ICurrentActor? userContext = null)
        : DbContext(options) {

        //..add user context to handle created or updated status
        private readonly ICurrentActor? _userContext = userContext;

        //..organization objects
        public DbSet<Organization> Organizations => Set<Organization>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<BranchHoliday> BranchHolidays => Set<BranchHoliday>();

        //..system access objects
        public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<PasswordHistory> PasswordHistories => Set<PasswordHistory>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<RoleGroup> RoleGroups => Set<RoleGroup>();
        public DbSet<RoleGroupMember> RoleGroupMembers => Set<RoleGroupMember>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<UserBranchAccess> UserBranchAccess => Set<UserBranchAccess>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        //..system Configuration
        public DbSet<SystemConfiguration> SystemConfigs => Set<SystemConfiguration>();
        public DbSet<SystemPolicy> SystemPolicies => Set<SystemPolicy>();
        public DbSet<RoleGroupPolicyOverride> RoleGroupPolicyOverrides => Set<RoleGroupPolicyOverride>();

        //..audit objects
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        //..KYC.Customers
        public DbSet<Individual> Individuals => Set<Individual>();
        public DbSet<Member> Members => Set<Member>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Business> Businesses => Set<Business>();
        public DbSet<Guarantor> Guarantors => Set<Guarantor>();

        //..KYC.Group structures
        public DbSet<Cluster> Clusters => Set<Cluster>();
        public DbSet<ClusterMember> ClusterMembers => Set<ClusterMember>();
        public DbSet<Meeting> Meetings => Set<Meeting>();

        //..KYC.Support entities
        public DbSet<CustomerExit> CustomerExits => Set<CustomerExit>();
        public DbSet<CustomerBlackList> CustomerBlackLists => Set<CustomerBlackList>();
        public DbSet<RejectedCustomer> RejectedCustomers => Set<RejectedCustomer>();
        public DbSet<UnlockedCustomer> UnlockedCustomers => Set<UnlockedCustomer>();
        public DbSet<CustomerApproval> CustomerApprovals => Set<CustomerApproval>();
        public DbSet<CustomerContact> CustomerContacts => Set<CustomerContact>();
        public DbSet<MemberTransfer> MemberTransfers => Set<MemberTransfer>();
        public DbSet<MemberPosition> MemberPositions => Set<MemberPosition>();
        public DbSet<IncomeHistory> IncomeHistories => Set<IncomeHistory>();
        public DbSet<EmploymentHistory> EmploymentHistories => Set<EmploymentHistory>();

        //..KYC.Files & Documents
        public DbSet<OtherFile> OtherFiles => Set<OtherFile>();
        public DbSet<TitleDeed> TitleDeeds => Set<TitleDeed>();
        public DbSet<CustomerAgreement> CustomerAgreements => Set<CustomerAgreement>();
        public DbSet<CustomerContract> CustomerContracts => Set<CustomerContract>();
        public DbSet<ImageFile> ImageFiles => Set<ImageFile>();
        public DbSet<Identification> Identifications => Set<Identification>();
        public DbSet<Signatory> Signatories => Set<Signatory>();
        public DbSet<SavingPartner> SavingPartners => Set<SavingPartner>();

        //..KYC.Reasons
        public DbSet<GeneralReason> GeneralReasons => Set<GeneralReason>();
        public DbSet<RejectReason> RejectReasons => Set<RejectReason>();

        //..KYC.Lookups
        public DbSet<Title> Titles => Set<Title>();
        public DbSet<Nationality> Nationalities => Set<Nationality>();
        public DbSet<Village> Villages => Set<Village>();
        public DbSet<Profession> Professions => Set<Profession>();
        public DbSet<Education> Educations => Set<Education>();
        public DbSet<IncomeType> IncomeTypes => Set<IncomeType>();
        public DbSet<GroupPosition> GroupPositions => Set<GroupPosition>();
        public DbSet<IdentificationType> IdentificationTypes => Set<IdentificationType>();
        public DbSet<IssuerAuthority> IssuerAuthorities => Set<IssuerAuthority>();
        public DbSet<CustomerFilter> CustomerFilters => Set<CustomerFilter>();

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
            var actor = _userContext?.IsAuthenticated == true
                ? _userContext.Username
                : "SYSTEM";

            foreach (var entry in ChangeTracker.Entries<BaseEntity>()) {
                switch (entry.State) {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        entry.Entity.CreatedBy ??= actor;  
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedOn = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = actor;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
