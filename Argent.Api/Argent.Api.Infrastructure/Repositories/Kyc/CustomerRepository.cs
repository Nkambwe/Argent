using Argent.Api.Domain.Entities.Kyc;
using Argent.Api.Domain.Entities.Kyc.KycBusinesses;
using Argent.Api.Domain.Entities.Kyc.KycGroups;
using Argent.Api.Domain.Entities.Kyc.KycIndividuals;
using Argent.Api.Domain.Entities.Support.KycLookup;
using Argent.Api.Domain.Entities.Support.KycSupport;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Argent.Api.Infrastructure.Repositories.Kyc {
    public class CustomerRepository(AppDataContext context) : ICustomerRepository {
        private readonly AppDataContext _context = context;

        public async Task<string> GenerateClientCodeAsync(
            CustomerType type, long branchId, CancellationToken ct = default) {
            // Prefix by type, suffix with padded count across all types in branch
            var prefix = type switch
            {
                CustomerType.Individual => "IND",
                CustomerType.Member => "MBR",
                CustomerType.Group => "GRP",
                CustomerType.Business => "BUS",
                _ => "CLT"
            };

            // Count existing records for this type in this branch (including deleted)
            var count = await _context.Set<CustomerBase>()
                .IgnoreQueryFilters()
                .Where(c => c.BranchId == branchId && c.ClientType == (ClientType)(int)type)
                .CountAsync(ct);

            return $"{prefix}-{branchId.ToString()[..4].ToUpper()}-{(count + 1):D5}";
        }

        public async Task<bool> ClientCodeExistsAsync(string clientCode, CancellationToken ct = default)
            => await _context.Set<CustomerBase>().IgnoreQueryFilters().AnyAsync(c => c.ClientCode == clientCode, ct);

        public async Task<Individual?> GetIndividualByIdAsync(long id, CancellationToken ct = default)
            => await _context.Individuals
                .Include(i => i.Branch)
                .Include(i => i.Title)
                .Include(i => i.Nationality)
                .Include(i => i.Profession)
                .Include(i => i.Education)
                .Include(i => i.Village)
                .Include(i => i.Filter1)
                .Include(i => i.Filter2)
                .Include(i => i.Filter3)
                .Include(i => i.Contacts)
                .Include(i => i.Identifications).ThenInclude(id => id.IdentityType)
                .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted, ct);

        public async Task<Individual?> GetIndividualByCodeAsync(string clientCode, CancellationToken ct = default)
            => await _context.Individuals.Include(i => i.Branch).FirstOrDefaultAsync(i => i.ClientCode == clientCode && !i.IsDeleted, ct);

        public async Task<(IEnumerable<Individual> Items, int Total)> GetIndividualsAsync(
            long? branchId, bool? active, bool? approved,
            string? search, int page, int pageSize, CancellationToken ct = default) {
            var query = _context.Individuals
                .Include(i => i.Branch)
                .Where(i => !i.IsDeleted);

            if (branchId.HasValue)
                query = query.Where(i => i.BranchId == branchId.Value);
            if (active.HasValue)
                query = query.Where(i => i.Active == active.Value);
            if (approved.HasValue)
                query = query.Where(i => i.Approved == approved.Value);
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(i =>
                    i.FirstName.Contains(search) ||
                    i.LastName.Contains(search) ||
                    i.ClientCode.Contains(search) ||
                    (i.Mobile != null && i.Mobile.Contains(search)) ||
                    (i.Email != null && i.Email.Contains(search)));

            var total = await query.CountAsync(ct);
            var items = await query.OrderByDescending(i => i.RegisteredOn).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);

            return (items, total);
        }

        public async Task AddIndividualAsync(Individual individual, CancellationToken ct = default)
            => await _context.Individuals.AddAsync(individual, ct);

        public void UpdateIndividual(Individual individual) {
            individual.UpdatedOn = DateTime.UtcNow;
            _context.Individuals.Update(individual);
        }

        public async Task<Member?> GetMemberByIdAsync(long id, CancellationToken ct = default)
            => await _context.Members
                .Include(m => m.Branch)
                .Include(m => m.Group)
                .Include(m => m.Title)
                .Include(m => m.Nationality)
                .Include(m => m.Profession)
                .Include(m => m.Education)
                .Include(m => m.Village)
                .Include(m => m.Contacts)
                .Include(m => m.Identifications).ThenInclude(id => id.IdentityType)
                .Include(m => m.Positions).ThenInclude(p => p.Position)
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted, ct);

        public async Task<(IEnumerable<Member> Items, int Total)> GetMembersByGroupAsync(
            long groupId, bool? active, int page, int pageSize, CancellationToken ct = default) {
            var query = _context.Members.Include(m => m.Branch).Where(m => m.GroupId == groupId && !m.IsDeleted);

            if (active.HasValue)
                query = query.Where(m => m.Active == active.Value);

            var total = await query.CountAsync(ct);
            var items = await query
                .OrderBy(m => m.LastName).ThenBy(m => m.FirstName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task AddMemberAsync(Member member, CancellationToken ct = default)
            => await _context.Members.AddAsync(member, ct);

        public void UpdateMember(Member member) {
            member.UpdatedOn = DateTime.UtcNow;
            _context.Members.Update(member);
        }

        public async Task<Group?> GetGroupByIdAsync(long id, CancellationToken ct = default)
            => await _context.Groups
                .Include(g => g.Branch)
                .Include(g => g.GroupFilter1)
                .Include(g => g.GroupFilter2)
                .Include(g => g.Filter1)
                .Include(g => g.Filter2)
                .Include(g => g.Filter3)
                .Include(g => g.Contacts)
                .FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted, ct);

        public async Task<Group?> GetGroupByCodeAsync(string clientCode, CancellationToken ct = default)
            => await _context.Groups.Include(g => g.Branch).FirstOrDefaultAsync(g => g.ClientCode == clientCode && !g.IsDeleted, ct);

        public async Task<(IEnumerable<Group> Items, int Total)> GetGroupsAsync(long? branchId, bool? active, bool? approved, string? search, int page, int pageSize, CancellationToken ct = default) {
            var query = _context.Groups.Include(g => g.Branch).Where(g => !g.IsDeleted);

            if (branchId.HasValue)
                query = query.Where(g => g.BranchId == branchId.Value);
            if (active.HasValue)
                query = query.Where(g => g.Active == active.Value);
            if (approved.HasValue)
                query = query.Where(g => g.Approved == approved.Value);
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(g =>
                    g.RegisteredName.Contains(search) ||
                    g.ClientCode.Contains(search));

            var total = await query.CountAsync(ct);
            var items = await query
                .OrderByDescending(g => g.RegisteredOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task AddGroupAsync(Group group, CancellationToken ct = default)
            => await _context.Groups.AddAsync(group, ct);

        public void UpdateGroup(Group group) {
            group.UpdatedOn = DateTime.UtcNow;
            _context.Groups.Update(group);
        }

        public async Task<int> GetGroupMemberCountAsync(long groupId, CancellationToken ct = default)
            => await _context.Members.CountAsync(m => m.GroupId == groupId && !m.IsDeleted, ct);

        public async Task<Business?> GetBusinessByIdAsync(long id, CancellationToken ct = default)
            => await _context.Businesses
                .Include(b => b.Branch)
                .Include(b => b.BusinessFilter1)
                .Include(b => b.BusinessFilter2)
                .Include(b => b.Filter1)
                .Include(b => b.Filter2)
                .Include(b => b.Filter3)
                .Include(b => b.Contacts)
                .Include(b => b.Signatories).ThenInclude(s => s.Identifications)
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted, ct);

        public async Task<Business?> GetBusinessByCodeAsync(string clientCode, CancellationToken ct = default)
            => await _context.Businesses.Include(b => b.Branch).FirstOrDefaultAsync(b => b.ClientCode == clientCode && !b.IsDeleted, ct);

        public async Task<(IEnumerable<Business> Items, int Total)> GetBusinessesAsync(
            long? branchId, bool? active, bool? approved,
            string? search, int page, int pageSize, CancellationToken ct = default) {
            var query = _context.Businesses
                .Include(b => b.Branch)
                .Where(b => !b.IsDeleted);

            if (branchId.HasValue)
                query = query.Where(b => b.BranchId == branchId.Value);
            if (active.HasValue)
                query = query.Where(b => b.Active == active.Value);
            if (approved.HasValue)
                query = query.Where(b => b.Approved == approved.Value);
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(b =>
                    b.LegalName.Contains(search) ||
                    b.ClientCode.Contains(search));

            var total = await query.CountAsync(ct);
            var items = await query
                .OrderByDescending(b => b.RegisteredOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task AddBusinessAsync(Business business, CancellationToken ct = default)
            => await _context.Businesses.AddAsync(business, ct);

        public void UpdateBusiness(Business business) {
            business.UpdatedOn = DateTime.UtcNow;
            _context.Businesses.Update(business);
        }

        public async Task<Guarantor?> GetGuarantorByIdAsync(long id, CancellationToken ct = default)
            => await _context.Guarantors
                .Include(g => g.Title)
                .Include(g => g.Nationality)
                .Include(g => g.Village)
                .Include(g => g.Profession)
                .Include(g => g.Identifications).ThenInclude(i => i.IdentityType)
                .FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted, ct);

        public async Task AddGuarantorAsync(Guarantor guarantor, CancellationToken ct = default)
            => await _context.Guarantors.AddAsync(guarantor, ct);

        public void UpdateGuarantor(Guarantor guarantor) {
            guarantor.UpdatedOn = DateTime.UtcNow;
            _context.Guarantors.Update(guarantor);
        }

        public async Task<CustomerBase?> GetCustomerBaseAsync(
            long id, CustomerType type, CancellationToken ct = default) {
            return type switch {
                CustomerType.Individual => await _context.Individuals
                    .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted, ct),
                CustomerType.Member => await _context.Members
                    .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted, ct),
                CustomerType.Group => await _context.Groups
                    .FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted, ct),
                CustomerType.Business => await _context.Businesses
                    .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted, ct),
                _ => null
            };
        }

        public async Task AddApprovalAsync(CustomerApproval approval, CancellationToken ct = default)
            => await _context.CustomerApprovals.AddAsync(approval, ct);

        public async Task AddBlacklistAsync(CustomerBlackList blacklist, CancellationToken ct = default)
            => await _context.CustomerBlackLists.AddAsync(blacklist, ct);

        public async Task AddExitAsync(CustomerExit exit, CancellationToken ct = default)
            => await _context.CustomerExits.AddAsync(exit, ct);

        public async Task AddRejectionAsync(RejectedCustomer rejection, CancellationToken ct = default)
            => await _context.RejectedCustomers.AddAsync(rejection, ct);

        public async Task AddUnlockAsync(UnlockedCustomer unlock, CancellationToken ct = default)
            => await _context.UnlockedCustomers.AddAsync(unlock, ct);

        public async Task AddContactAsync(CustomerContact contact, CancellationToken ct = default)
            => await _context.CustomerContacts.AddAsync(contact, ct);

        public async Task<bool> IsBlacklistedAsync(
            long customerId, CustomerType type, CancellationToken ct = default)
            => await _context.CustomerBlackLists.AnyAsync(b => b.CustomerId == customerId 
                            && b.CustomerType == type
                            && b.UnListedOn == null && !b.IsDeleted, ct);

        public async Task<bool> HasPendingApprovalAsync(
            long customerId, CustomerType type, CancellationToken ct = default)
            => await _context.CustomerApprovals.AnyAsync(a => a.CustomerId == customerId
                            && a.CustomerType == type
                            && a.Status == ApprovalStatus.Pending
                            && !a.IsDeleted, ct);

        public async Task<IEnumerable<Title>> GetTitlesAsync(CancellationToken ct = default)
            => await _context.Titles.Where(t => !t.IsDeleted).OrderBy(t => t.Name).ToListAsync(ct);

        public async Task<IEnumerable<Nationality>> GetNationalitiesAsync(CancellationToken ct = default)
            => await _context.Nationalities.Where(n => !n.IsDeleted).OrderBy(n => n.Name).ToListAsync(ct);

        public async Task<IEnumerable<Village>> GetVillagesAsync(
            string? search, CancellationToken ct = default) {
            var query = _context.Villages.Where(v => !v.IsDeleted);
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(v =>
                    v.Name.Contains(search) ||
                    v.District!.Contains(search) ||
                    v.Parish!.Contains(search));
            return await query.OrderBy(v => v.Name).Take(100).ToListAsync(ct);
        }

        public async Task<IEnumerable<Profession>> GetProfessionsAsync(CancellationToken ct = default)
            => await _context.Professions.Where(p => !p.IsDeleted).OrderBy(p => p.Name).ToListAsync(ct);

        public async Task<IEnumerable<Education>> GetEducationLevelsAsync(CancellationToken ct = default)
            => await _context.Educations.Where(e => !e.IsDeleted).OrderBy(e => e.Name).ToListAsync(ct);

        public async Task<IEnumerable<IncomeType>> GetIncomeTypesAsync(CancellationToken ct = default)
            => await _context.IncomeTypes.Where(i => !i.IsDeleted).OrderBy(i => i.Name).ToListAsync(ct);

        public async Task<IEnumerable<GroupPosition>> GetGroupPositionsAsync(CancellationToken ct = default)
            => await _context.GroupPositions.Where(p => !p.IsDeleted).OrderBy(p => p.Designation).ToListAsync(ct);

        public async Task<IEnumerable<IdentificationType>> GetIdentificationTypesAsync(CancellationToken ct = default)
            => await _context.IdentificationTypes
                .Where(i => !i.IsDeleted)
                .OrderByDescending(i => i.Required)
                .ThenBy(i => (int)i.Priority)
                .ToListAsync(ct);

        public async Task<IEnumerable<IssuerAuthority>> GetIssuerAuthoritiesAsync(CancellationToken ct = default)
            => await _context.IssuerAuthorities.Where(a => !a.IsDeleted).OrderBy(a => a.Name).ToListAsync(ct);

        public async Task<IEnumerable<CustomerFilter>> GetFiltersAsync(
            FilterScope scope, int slot, CancellationToken ct = default)
            => await _context.CustomerFilters
                .Where(f => f.Scope == scope && f.SlotNumber == slot && f.IsActive && !f.IsDeleted)
                .OrderBy(f => f.Description)
                .ToListAsync(ct);

        public async Task<IEnumerable<GeneralReason>> GetReasonsAsync(
            string category, CancellationToken ct = default)
            => await _context.GeneralReasons
                .Where(r => r.Category == category && !r.IsDeleted)
                .OrderBy(r => r.Description)
                .ToListAsync(ct);

        public async Task<IEnumerable<RejectReason>> GetRejectReasonsAsync(CancellationToken ct = default)
            => await _context.RejectReasons.Where(r => !r.IsDeleted).OrderBy(r => r.Description).ToListAsync(ct);
    }
}
