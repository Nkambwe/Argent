using Argent.Api.Domain.Entities.Kyc;
using Argent.Api.Domain.Entities.Kyc.KycBusinesses;
using Argent.Api.Domain.Entities.Kyc.KycGroups;
using Argent.Api.Domain.Entities.Kyc.KycIndividuals;
using Argent.Api.Domain.Entities.Support.KycLookup;
using Argent.Api.Domain.Entities.Support.KycSupport;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Repositories.Kyc {

    public interface ICustomerRepository {
        /// <summary>
        /// Generates the next client code based on SystemConfig format settings.
        /// Thread-safe — uses a sequence query so concurrent registrations don't collide.
        /// </summary>
        Task<string> GenerateClientCodeAsync(CustomerType type, long branchId, CancellationToken ct = default);
        Task<bool> ClientCodeExistsAsync(string clientCode, CancellationToken ct = default);
        Task<Individual?> GetIndividualByIdAsync(long id, CancellationToken ct = default);
        Task<Individual?> GetIndividualByCodeAsync(string clientCode, CancellationToken ct = default);
        Task<(IEnumerable<Individual> Items, int Total)> GetIndividualsAsync(long? branchId, bool? active, bool? approved, string? search, int page, int pageSize, CancellationToken ct = default);
        Task AddIndividualAsync(Individual individual, CancellationToken ct = default);
        void UpdateIndividual(Individual individual);
        Task<Member?> GetMemberByIdAsync(long id, CancellationToken ct = default);
        Task<(IEnumerable<Member> Items, int Total)> GetMembersByGroupAsync(long groupId, bool? active, int page, int pageSize, CancellationToken ct = default);
        Task AddMemberAsync(Member member, CancellationToken ct = default);
        void UpdateMember(Member member);
        Task<Group?> GetGroupByIdAsync(long id, CancellationToken ct = default);
        Task<Group?> GetGroupByCodeAsync(string clientCode, CancellationToken ct = default);
        Task<(IEnumerable<Group> Items, int Total)> GetGroupsAsync(long? branchId, bool? active, bool? approved, string? search, int page, int pageSize, CancellationToken ct = default);
        Task AddGroupAsync(Group group, CancellationToken ct = default);
        void UpdateGroup(Group group);
        Task<int> GetGroupMemberCountAsync(long groupId, CancellationToken ct = default);
        Task<Business?> GetBusinessByIdAsync(long id, CancellationToken ct = default);
        Task<Business?> GetBusinessByCodeAsync(string clientCode, CancellationToken ct = default);
        Task<(IEnumerable<Business> Items, int Total)> GetBusinessesAsync(long? branchId, bool? active, bool? approved, string? search, int page, int pageSize, CancellationToken ct = default);
        Task AddBusinessAsync(Business business, CancellationToken ct = default);
        void UpdateBusiness(Business business);
        Task<Guarantor?> GetGuarantorByIdAsync(long id, CancellationToken ct = default);
        Task AddGuarantorAsync(Guarantor guarantor, CancellationToken ct = default);
        void UpdateGuarantor(Guarantor guarantor);
        Task<CustomerBase?> GetCustomerBaseAsync(long id, CustomerType type, CancellationToken ct = default);
        Task AddApprovalAsync(CustomerApproval approval, CancellationToken ct = default);
        Task AddBlacklistAsync(CustomerBlackList blacklist, CancellationToken ct = default);
        Task AddExitAsync(CustomerExit exit, CancellationToken ct = default);
        Task AddRejectionAsync(RejectedCustomer rejection, CancellationToken ct = default);
        Task AddUnlockAsync(UnlockedCustomer unlock, CancellationToken ct = default);
        Task AddContactAsync(CustomerContact contact, CancellationToken ct = default);
        Task<bool> IsBlacklistedAsync(long customerId, CustomerType type, CancellationToken ct = default);
        Task<bool> HasPendingApprovalAsync(long customerId, CustomerType type, CancellationToken ct = default);
        Task<IEnumerable<Title>> GetTitlesAsync(CancellationToken ct = default);
        Task<IEnumerable<Nationality>> GetNationalitiesAsync(CancellationToken ct = default);
        Task<IEnumerable<Village>> GetVillagesAsync(string? search, CancellationToken ct = default);
        Task<IEnumerable<Profession>> GetProfessionsAsync(CancellationToken ct = default);
        Task<IEnumerable<Education>> GetEducationLevelsAsync(CancellationToken ct = default);
        Task<IEnumerable<IncomeType>> GetIncomeTypesAsync(CancellationToken ct = default);
        Task<IEnumerable<GroupPosition>> GetGroupPositionsAsync(CancellationToken ct = default);
        Task<IEnumerable<IdentificationType>> GetIdentificationTypesAsync(CancellationToken ct = default);
        Task<IEnumerable<IssuerAuthority>> GetIssuerAuthoritiesAsync(CancellationToken ct = default);
        Task<IEnumerable<CustomerFilter>> GetFiltersAsync(FilterScope scope, int slot, CancellationToken ct = default);
        Task<IEnumerable<GeneralReason>> GetReasonsAsync(string category, CancellationToken ct = default);
        Task<IEnumerable<RejectReason>> GetRejectReasonsAsync(CancellationToken ct = default);
    }
}
