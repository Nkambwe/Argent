using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities;
using Argent.Api.Domain.Entities.Access;
using Argent.Api.Domain.Entities.Accounting;
using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Argent.Api.Domain.Entities.Accounting.Charges;
using Argent.Api.Domain.Entities.Accounting.Currencies;
using Argent.Api.Domain.Entities.Accounting.Documents;
using Argent.Api.Domain.Entities.Accounting.Journals;
using Argent.Api.Domain.Entities.Accounting.Postings;
using Argent.Api.Domain.Entities.Accounting.Taxes;
using Argent.Api.Domain.Entities.Accounting.Vouchers;
using Argent.Api.Domain.Entities.Audit;
using Argent.Api.Domain.Entities.Banking;
using Argent.Api.Domain.Entities.Banking.Loans;
using Argent.Api.Domain.Entities.Banking.Savings;
using Argent.Api.Domain.Entities.Kyc;
using Argent.Api.Domain.Entities.Kyc.KycBusinesses;
using Argent.Api.Domain.Entities.Kyc.KycFiles;
using Argent.Api.Domain.Entities.Kyc.KycGroups;
using Argent.Api.Domain.Entities.Kyc.KycIndividuals;
using Argent.Api.Domain.Entities.Settings;
using Argent.Api.Domain.Entities.Support;
using Argent.Api.Domain.Entities.Support.KycLookup;
using Argent.Api.Domain.Entities.Support.KycSupport;
using Argent.Api.Domain.Entities.Vendors;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Data.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using System.Reflection;

namespace Argent.Api.Infrastructure.Data {
    public class AppDataContext(DbContextOptions<AppDataContext> options, 
        ICurrentActor? userContext = null,
        IEncryptionService? encryption = null)
        : DbContext(options) {

        //..add user context to handle created or updated status
        private readonly ICurrentActor? _userContext = userContext;
        private readonly IEncryptionService? _encryption = encryption;

        //..organization objects
        public DbSet<Organization> Organizations => Set<Organization>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<BranchHoliday> BranchHolidays => Set<BranchHoliday>();
        public DbSet<BranchLedgerAccount> BranchLedgerAccounts => Set<BranchLedgerAccount>();
        public DbSet<BranchReference> BranchReferences => Set<BranchReference>();

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

        //..accounting core
        public DbSet<AccountsChart> AccountsCharts => Set<AccountsChart>();
        public DbSet<LedgerAccountHeader> LedgerAccountHeaders => Set<LedgerAccountHeader>();
        public DbSet<LedgerAccount> LedgerAccounts => Set<LedgerAccount>();
        public DbSet<LedgerAccountTotal> LedgerAccountTotals => Set<LedgerAccountTotal>();
        public DbSet<FolioType> FolioTypes => Set<FolioType>();
        public DbSet<Folio> Folios => Set<Folio>();
        public DbSet<AccountReference> AccountReferences => Set<AccountReference>();
        public DbSet<AccountReferenceValue> AccountReferenceValues => Set<AccountReferenceValue>();
        public DbSet<LedgerAccountReference> LedgerAccountReferences => Set<LedgerAccountReference>();
        public DbSet<FinancialYear> FinancialYears => Set<FinancialYear>();
        public DbSet<MonthlyClosure> MonthlyClosures => Set<MonthlyClosure>(); 
        public DbSet<SeriesNumber> SeriesNumbers => Set<SeriesNumber>();
        public DbSet<RevenueCenter> RevenueCenters => Set<RevenueCenter>();
        public DbSet<CostCenter> CostCenters => Set<CostCenter>();

        //..posting groups
        public DbSet<BranchPostingGroup> BranchPostingGroups => Set<BranchPostingGroup>();
        public DbSet<BusinessPostingGroup> BusinessPostingGroups => Set<BusinessPostingGroup>();
        public DbSet<GeneralPostingGroup> GeneralPostingGroups => Set<GeneralPostingGroup>();

        //..cashflows
        public DbSet<Cashier> Cashiers => Set<Cashier>();
        public DbSet<CashierAccount> CashierAccounts => Set<CashierAccount>(); 
        public DbSet<CashierBranchAccess> CashierBranchAccesses => Set<CashierBranchAccess>();
        public DbSet<Iban> Ibans => Set<Iban>();
        public DbSet<Swift> Swifts => Set<Swift>();
        public DbSet<Bank> Banks => Set<Bank>();
        public DbSet<BankBranch> BankBranches => Set<BankBranch>();
        public DbSet<BankAccount> BankAccounts => Set<BankAccount>();
        public DbSet<ChequeBook> ChequeBooks => Set<ChequeBook>();
        public DbSet<Cheque> Cheques => Set<Cheque>();
        public DbSet<Card> Cards => Set<Card>();

        //..currencies
        public DbSet<Currency> Currencies => Set<Currency>();
        public DbSet<Denomination> Denominations => Set<Denomination>();
        public DbSet<ExchangeRate> ExchangeRates => Set<ExchangeRate>();
        public DbSet<BankAccountCurrency> BankAccountCurrencies => Set<BankAccountCurrency>();

        //..tax 
        public DbSet<Tax> Taxes => Set<Tax>();
        public DbSet<TaxableItem> TaxableItems => Set<TaxableItem>();
        public DbSet<TaxGroup> TaxGroups => Set<TaxGroup>();
        public DbSet<VendorTax> VendorTaxes => Set<VendorTax>();
        public DbSet<JournalTypeTaxGroup> JournalTypeTaxGroups => Set<JournalTypeTaxGroup>();

        //..ledgers
        public DbSet<CardLedgerEntry> CardLedgerEntries => Set<CardLedgerEntry>(); 
        public DbSet<RegistrationLedgerEntry> RegistrationLedgerEntries => Set<RegistrationLedgerEntry>();
        public DbSet<ChargeLedgerEntry> ChargeLedgerEntries => Set<ChargeLedgerEntry>();
        public DbSet<BankLedgerEntry> BankLedgerEntries => Set<BankLedgerEntry>(); 
        public DbSet<GeneralLedgerEntry> GeneralLedgerEntries => Set<GeneralLedgerEntry>();
        public DbSet<LedgerRecurringItem> RecurringItems => Set<LedgerRecurringItem>();

        //..vouchers and Journals
        public DbSet<VoucherType> VoucherTypes => Set<VoucherType>(); 
        public DbSet<VoucherEntry> VoucherLines => Set<VoucherEntry>(); 
        public DbSet<CashierVoucherType> CashierVoucherTypes => Set<CashierVoucherType>();
        public DbSet<JournalType> JournalTypes => Set<JournalType>();
        public DbSet<JournalEntry> JournalEntry => Set<JournalEntry>();
        
        //..vendors 
        public DbSet<Vendor> Vendors => Set<Vendor>();
        public DbSet<VendorGroup> VendorGroups => Set<VendorGroup>();
        public DbSet<VendorBankAccount> VendorBankAccounts => Set<VendorBankAccount>();
        public DbSet<VendorReference> VendorReferences => Set<VendorReference>(); 
        public DbSet<VendorItemGroup> VendorItemGroups => Set<VendorItemGroup>();
        public DbSet<DeliveryTerms> DeliveryTerms => Set<DeliveryTerms>();
        public DbSet<DeliveryDefaults> DeliveryDefaults => Set<DeliveryDefaults>();
        public DbSet<InvoicingDefault> InvoicingDefaults => Set<InvoicingDefault>();
        public DbSet<DeliveryMode> DeliveryModes => Set<DeliveryMode>();

        //..purchases 
        public DbSet<DiscountGroup> DiscountGroups => Set<DiscountGroup>();
        public DbSet<PriceGroup> PriceGroups => Set<PriceGroup>();
        public DbSet<PurchasingDefault> PurchasingDefaults => Set<PurchasingDefault>();
        public DbSet<PurchaseOrderDefault> PurchaseOrderDefaults => Set<PurchaseOrderDefault>();
        public DbSet<PurchaseOrderClassification> PurchaseOrderClassifications => Set<PurchaseOrderClassification>();

        //..Accounting charges
        public DbSet<ChargeGroup> ChargeGroups => Set<ChargeGroup>();
        public DbSet<ChargeGroupItem> ChargeGroupItems => Set<ChargeGroupItem>();
        public DbSet<Charge> Charges => Set<Charge>();
        public DbSet<ChargeItem> ChargeItems => Set<ChargeItem>();
        public DbSet<ChargeItemCharge> ChargeItemCharges => Set<ChargeItemCharge>();
        
        //..accounting documents
        public DbSet<TransactionDocumentType> TransactionDocumentTypes => Set<TransactionDocumentType>();
        public DbSet<TransactionDocument> TransactionDocuments => Set<TransactionDocument>();

        //..banking
        public DbSet<Teller> Tellers => Set<Teller>();
        public DbSet<TellerLedgerAccount> TellerLedgerAccounts => Set<TellerLedgerAccount>();

        //..loans
        public DbSet<LoanChargeStage> LoanChargeStages => Set<LoanChargeStage>();
        public DbSet<LoanOfficer> LoanOfficers => Set<LoanOfficer>();
        public DbSet<LoanOfficerLedgerAccount> LoanOfficerLedgerAccounts => Set<LoanOfficerLedgerAccount>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            //..apply all entity configurations from this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDataContext).Assembly);

            //..default schema — all tables live under the "mfi" schema in PostgreSQL
            modelBuilder.HasDefaultSchema("mfi");

            //..all entities with soft delete are filtered automatically
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType) && entityType.BaseType == null) {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var body = Expression.Equal(Expression.Property(parameter, nameof(BaseEntity.IsDeleted)), Expression.Constant(false));
                    var lambda = Expression.Lambda(body, parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }

                //..field-level encryption for [Encryptable] string properties.
                if (_encryption is null)
                    continue;

                var converter = new EncryptedStringConverter(_encryption);

                foreach (var property in entityType.GetProperties()) {
                    if (property.ClrType != typeof(string)) continue;

                    var memberInfo = property.PropertyInfo ?? (MemberInfo?)property.FieldInfo;
                    if (memberInfo is null) continue;

                    var isEncryptable = memberInfo.GetCustomAttributes(typeof(EncryptableAttribute), inherit: true).Length != 0;
                    if (!isEncryptable)
                        continue;

                    property.SetValueConverter(converter);

                    // ..override any MaxLength set in configurations to prevent truncation.
                    // 500 accommodates AES-256 + Base64 overhead for strings up to ~350 chars.
                    if ((property.GetMaxLength() ?? 0) < 500)
                        property.SetMaxLength(500);
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
