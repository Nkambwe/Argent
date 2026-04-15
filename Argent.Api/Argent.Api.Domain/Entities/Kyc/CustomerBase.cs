using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Kyc.KycFiles;
using Argent.Api.Domain.Entities.Support.KycLookup;
using Argent.Api.Domain.Entities.Support.KycSupport;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Kyc {

    /// <summary>
    /// Abstract base for all customer types: Individual, Member, Group, Business.
    ///</summery>
    ///<remarks>
    /// Contains every field shared across all four types.  Each concrete type adds only what is unique to it.
    /// Branch-awareness: all customers belong to a branch. This is the FK
    /// that drives branch-level data isolation and access control.
    /// </remarks>
    public abstract class CustomerBase : BaseEntity {

        /// <summary>
        /// System-generated client code. Format governed by SystemConfig.
        /// </summary>
        public string ClientCode { get; set; } = string.Empty;

        /// <summary>
        /// Optional statistical reference number.
        /// </summary>
        public string? Statistic { get; set; }

        /// <summary>
        /// Optional client reference number (external system link).
        /// </summary>
        public string? Reference { get; set; }

        public long BranchId { get; set; }
        public Branch Branch { get; set; } = null!;

        [Encryptable("Permanent Address")]
        public string? PermanentAddress { get; set; }

        [Encryptable("Mail Address")]
        public string? MailAddress { get; set; }

        [Encryptable("Primary Phone")]
        public string? PrimaryLine { get; set; }

        [Encryptable("Secondary Phone")]
        public string? SecondaryLine { get; set; }

        [Encryptable("Mobile")]
        public string? Mobile { get; set; }

        [Encryptable("Fax")]
        public string? Fax { get; set; }

        [Encryptable("Email")]
        public string? Email { get; set; }

        [Encryptable("City")]
        public string? City { get; set; }

        [Encryptable("Town")]
        public string? Town { get; set; }

        [Encryptable("WhatsApp")]
        public string? WhatsApp { get; set; }

        [Encryptable("Facebook")]
        public string? Facebook { get; set; }

        [Encryptable("Instagram")]
        public string? Instagram { get; set; }

        [Encryptable("Twitter/X")]
        public string? Twitter { get; set; }

        public ClientType ClientType { get; set; }

        public DateTime RegisteredOn { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = false;        
        public bool Approved { get; set; } = false;
        public DateTime? ApprovedOn { get; set; }
        public string? ApprovedBy { get; set; }
        public bool Exited { get; set; } = false;
        public DateTime? ExitedOn { get; set; }

        /// <summary>
        /// When false, customer cannot initiate transactions (e.g. pending fees, pending approval).
        /// </summary>
        public bool CanTransact { get; set; } = false;

        /// <summary>
        /// Customer can hold shares.
        /// </summary>
        public bool HoldShares { get; set; } = false;

        public long? Filter1Id { get; set; }
        public CustomerFilter? Filter1 { get; set; }

        public long? Filter2Id { get; set; }
        public CustomerFilter? Filter2 { get; set; }

        public long? Filter3Id { get; set; }
        public CustomerFilter? Filter3 { get; set; }

        public long? VillageId { get; set; }
        public Village? Village { get; set; }

        [Encryptable("Notes")]
        public string? Notes { get; set; }

        public ICollection<CustomerApproval> Approvals { get; set; } = [];
        public ICollection<CustomerContact> Contacts { get; set; } = [];
        public ICollection<CustomerBlackList> BlackLists { get; set; } = [];
        public ICollection<CustomerAgreement> Agreements { get; set; } = [];
        public ICollection<CustomerContract> Contracts { get; set; } = [];
        public ICollection<TitleDeed> TitleDeeds { get; set; } = [];
        public ICollection<OtherFile> Files { get; set; } = [];
        public CustomerExit? Exit { get; set; }
        public ICollection<RejectedCustomer> Rejections { get; set; } = [];
        public ICollection<UnlockedCustomer> Unlocks { get; set; } = [];
    }

}
